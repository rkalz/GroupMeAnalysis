using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace GroupMeAnalysis {
    public class Group {
        [JsonProperty(PropertyName = "id")]
        public long Id {get; set;}

        [JsonProperty(PropertyName = "name")]
        public string Name {get; set;}

        [JsonProperty(PropertyName = "type")]
        public string Type {get; set;}

        [JsonProperty(PropertyName = "description")]
        public string Description {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {get; set;}

        [JsonProperty(PropertyName = "creator_user_id")]
        public long CreatorUserId {get; set;}

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

        public override string ToString() {
            return this.Id.ToString() + " - " + this.Name;
        }
    }

    public class GroupMember {
        [JsonProperty(PropertyName = "user_id")]
        public long UserId {get; set;}

        [JsonProperty(PropertyName = "nickname")]
        public string Nickname {get; set;}

        [JsonProperty(PropertyName = "muted")]
        public bool Muted {get; set;}

        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl {get; set;}
    }
}