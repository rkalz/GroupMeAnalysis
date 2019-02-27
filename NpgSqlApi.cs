using System;
using System.Threading.Tasks;

using Npgsql;

namespace GroupMeAnalysis {
    static class NpgSqlApi {
        public static Task AsyncAddOrUpdateGroup(Group group) {
            string[] members = new string[group.Members.Count];
            for (int i = 0; i < group.Members.Count; i++) {
                members[i] = group.Members[i].UserId;
            }
            var sh_url = group.ShareUrl == null ? "" : group.ShareUrl;

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
                        cmd.Parameters.AddWithValue("desc", group.Description);
                        cmd.Parameters.AddWithValue("img_url", group.ImageUrl);
                        cmd.Parameters.AddWithValue("cu_id", group.CreatorUserId);
                        cmd.Parameters.AddWithValue("created", group.CreatedAt);
                        cmd.Parameters.AddWithValue("updated", group.UpdatedAt);
                        cmd.Parameters.AddWithValue("members", members);
                        cmd.Parameters.AddWithValue("sh_url", sh_url);

                        var result = cmd.ExecuteNonQuery();
                    }
                }
            });
            task.Start();
            return task;
        }
    }
}