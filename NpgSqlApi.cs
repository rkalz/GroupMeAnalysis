using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Npgsql;

namespace GroupMeAnalysis {
    static class NpgSqlApi {
        public static Task AsyncAddOrUpdateGroup(Group group) {
            var members = new List<string>();
            group.Members.ForEach(m => members.Add(m.UserId));
            var shareUrl = group.ShareUrl == null ? "" : group.ShareUrl;
            var imgUrl = group.ImageUrl == null ? "" : group.ImageUrl;
            var desc = group.Description == null ? "" : group.Description;

            var task = new Task(() => {
                using (var conn = new NpgsqlConnection(Secret.ConnString)) {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand()) {
                        cmd.Connection = conn;
                        cmd.CommandText = @"INSERT INTO public.groups
                            (id, name, type, description, image_url, creator_user_id,
                            created_at, updated_at, members, share_url)
                            VALUES (@id, @name, @type, @desc, @img_url, @cu_id,
                            @created, @updated, @members, @sh_url)
                            ON CONFLICT (id) DO UPDATE
                            SET name = @name, type = @type, description = @desc,
                            image_url = @img_url, updated_at = @updated, members = @members,
                            share_url = @sh_url";

                        cmd.Parameters.AddWithValue("id", group.Id);
                        cmd.Parameters.AddWithValue("name", group.Name);
                        cmd.Parameters.AddWithValue("type", group.Type);
                        cmd.Parameters.AddWithValue("desc", desc);
                        cmd.Parameters.AddWithValue("img_url", imgUrl);
                        cmd.Parameters.AddWithValue("cu_id", group.CreatorUserId);
                        cmd.Parameters.AddWithValue("created", group.CreatedAt);
                        cmd.Parameters.AddWithValue("updated", group.UpdatedAt);
                        cmd.Parameters.AddWithValue("members", members);
                        cmd.Parameters.AddWithValue("sh_url", shareUrl);

                        var result = cmd.ExecuteNonQuery();
                    }
                }
            });
            task.Start();
            return task;
        }

        public static Task<string> GetNewestGroupMessageAsync(Group group) {
            var task = new Task<string>(() => {
                string id = null;
                using (var conn = new NpgsqlConnection(Secret.ConnString)) {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand()) {
                        cmd.Connection = conn;
                        cmd.CommandText = @"SELECT id
                        FROM public.messages
                        WHERE group_id = @gid
                        ORDER BY created_at DESC
                        LIMIT 1";

                        cmd.Parameters.AddWithValue("gid", group.Id);

                        using (var reader = cmd.ExecuteReader()) {
                            if (reader.Read()) id = reader.GetString(0);
                        }
                    }
                }
                return id;
            });
            task.Start();
            return task;
        }

        public static Task<string> GetOldestGroupMessageAsync(Group group) {
            var task = new Task<string>(() => {
                string id = null;
                using (var conn = new NpgsqlConnection(Secret.ConnString)) {
                    conn.Open();

                    using (var cmd = new NpgsqlCommand()) {
                        cmd.Connection = conn;
                        cmd.CommandText = @"SELECT id
                        FROM public.messages
                        WHERE group_id = @gid
                        ORDER BY created_at ASC
                        LIMIT 1";

                        cmd.Parameters.AddWithValue("gid", group.Id);

                        using (var reader = cmd.ExecuteReader()) {
                            if (reader.Read()) id = reader.GetString(0);
                        }
                    }
                }
                return id;
            });
            task.Start();
            return task;
        }

        public static Task AddMessagesToDatabaseAsync(Group group, List<Message> messages) {
            var task = new Task(() => {
                messages.ForEach((Message m) => {
                    var favoritees = new List<string>();
                    m.FavoritedBy.ForEach(f => favoritees.Add(f.ToString()));

                    var mentions = new List<string>();
                    var attachTypes = new List<string>();
                    var urls = new List<string>();
                    var locis = new List<string>();

                    var text = m.Text == null ? "" : m.Text;

                    m.Attachments.ForEach((MessageAttachment ma) => {
                        if (ma.Mentions != null) mentions.AddRange(ma.Mentions);
                        attachTypes.Add(ma.Type);
                        if (ma.Url != null) urls.Add(ma.Url);
                        if (ma.Loci != null) ma.Loci.ForEach(l => l.ForEach(i => locis.Add(i.ToString())));
                    });

                    using (var conn = new NpgsqlConnection(Secret.ConnString)) {
                        conn.Open();

                        using (var cmd = new NpgsqlCommand()) {
                            cmd.Connection = conn;
                            cmd.CommandText = @"INSERT INTO public.messages
                            (id, source_guid, created_at, user_id, group_id,
                            sender_id, sender_type, name, system, favorited_by,
                            attachment_mentions, attachment_types, attachment_urls,
                            attachment_locis, message)
                            VALUES (@id, @guid, @created, @user, @group, @sender,
                            @sender_type, @name, @system, @favorited, @mentions,
                            @types, @urls, @loci, @message)
                            ON CONFLICT (id) DO UPDATE
                            SET favorited_by = @favorited";

                            cmd.Parameters.AddWithValue("id", m.Id);
                            cmd.Parameters.AddWithValue("guid", m.SourceGuid);
                            cmd.Parameters.AddWithValue("created", m.CreatedAt);
                            cmd.Parameters.AddWithValue("user", m.UserId);
                            cmd.Parameters.AddWithValue("group", group.Id);
                            cmd.Parameters.AddWithValue("sender", m.SenderId);
                            cmd.Parameters.AddWithValue("sender_type", m.SenderType);
                            cmd.Parameters.AddWithValue("name", m.Name);
                            cmd.Parameters.AddWithValue("system", m.System);
                            cmd.Parameters.AddWithValue("favorited", favoritees);
                            cmd.Parameters.AddWithValue("mentions", mentions);
                            cmd.Parameters.AddWithValue("types", attachTypes);
                            cmd.Parameters.AddWithValue("urls", urls);
                            cmd.Parameters.AddWithValue("loci", locis);
                            cmd.Parameters.AddWithValue("message", text);

                            cmd.ExecuteNonQuery();
                        }
                    }
                });
            });
            task.Start();
            return task;
        }
    }
}