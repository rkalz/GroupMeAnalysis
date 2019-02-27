using System;
using System.Collections.Generic;
using System.ComponentModel;

using Newtonsoft.Json;

namespace GroupMeAnalysis {
    public class Group {
        [JsonProperty(PropertyName = "id")]
        public string Id {get; set;}

        [JsonProperty(PropertyName = "name")]
        public string Name {get; set;}

        [JsonProperty(PropertyName = "type")]
        public string Type {get; set;}

        [JsonProperty(PropertyName = "description")]
        public string Description {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {get; set;}

        [JsonProperty(PropertyName = "creator_user_id")]
        public string CreatorUserId {get; set;}

        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
        public DateTime CreatedAt {get; set;}

        [JsonProperty(PropertyName = "updated_at")]
        [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
        public DateTime UpdatedAt {get; set;}

        [JsonProperty(PropertyName = "members")]
        public List<GroupMember> Members {get; set;}

        [JsonProperty(PropertyName = "share_url")]
        public string ShareUrl {get; set;}

        [JsonProperty(PropertyName = "messages")]
        public GroupMessageInfo MessageInfo {get; set;}

        public override string ToString() {
            return this.Id + " - " + this.Name;
        }
    }

    public class GroupMember {
        [JsonProperty(PropertyName = "user_id")]
        public string UserId {get; set;}

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname {get; set;}

        [JsonProperty(PropertyName = "muted")]
        public bool Muted {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {get; set;}

        public override string ToString() {
            return this.UserId.ToString() + " - " + this.Nickname;
        }
    }

    public class GroupMessageInfo {
        [JsonProperty(PropertyName = "count")]
        public int Count {get; set;}

        [JsonProperty(PropertyName = "last_message_id")]
        public string LastMessageId {get; set;}

        [JsonProperty(PropertyName = "last_message_created_at")]
        [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
        public DateTime LastMessageCreatedAt {get; set;}

        [JsonProperty(PropertyName = "preview")]
        public MessagePreview Preview {get; set;}
    }

    public class MessagePreview {
        [JsonProperty(PropertyName = "nickname")]
        public string Nickname {get; set;}

        [JsonProperty(PropertyName = "text")]
        public string Text {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {get; set;}

        [JsonProperty(PropertyName = "attachments")]
        public List<MessageAttachment> Attachments {get; set;}
    }

    public class Message {
        [JsonProperty(PropertyName = "id")]
        public string Id {get; set;}

        [JsonProperty(PropertyName = "source_guid")]
        public string SourceGuid {get; set;}

        [JsonProperty(PropertyName = "created_at")]
        [JsonConverter(typeof(UnixSecondsToDateTimeConverter))]
        public DateTime CreatedAt {get; set;}

        [JsonProperty(PropertyName = "user_id")]
        public string UserId {get; set;}

        [JsonProperty(PropertyName = "group_id")]
        public string GroupId {get; set;}

        [JsonProperty(PropertyName = "sender_id")]
        public string SenderId {get; set;}

        [JsonProperty(PropertyName = "sender_type")]
        public string SenderType {get; set;}

        [JsonProperty(PropertyName = "name")]
        public string Name {get; set;}

        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl {get; set;}

        [JsonProperty(PropertyName = "text")]
        public string Text {get; set;}

        [JsonProperty(PropertyName = "system")]
        public bool System {get; set;}

        [JsonProperty(PropertyName = "favorited_by")]
        public List<long> FavoritedBy {get; set;}

        [JsonProperty(PropertyName = "attachments")]
        public List<MessageAttachment> Attachments {get; set;}

        public override string ToString() {
            return this.Name + ": " + this.Text;
        }

    }

    public class MessageAttachment {
        [JsonProperty(PropertyName = "type")]
        public string Type {get; set;}

        [JsonProperty(PropertyName = "url")]
        public string Url {get; set;}

        [JsonProperty(PropertyName = "lat")]
        public string Latitude {get; set;}

        [JsonProperty(PropertyName = "lon")]
        public string Longitude {get; set;}

        [JsonProperty(PropertyName = "name")]
        public string LocationName {get; set;}

        [JsonProperty(PropertyName = "token")]
        public string SplitToken {get; set;}

        [JsonProperty(PropertyName = "placeholder")]
        public string EmojiPlaceholder {get; set;}

        [JsonProperty(PropertyName = "charmap")]
        public List<List<int>> Charmap {get; set;}

        [JsonProperty(PropertyName = "loci")]
        public List<List<int>> Loci {get; set;}

        [JsonProperty(PropertyName = "mentions")]
        public List<string> Mentions {get; set;}
    }
}