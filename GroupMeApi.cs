using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace groupme_analysis {
    static class GroupMeApi {
        private const string ApiEndpoint = "https://api.groupme.com/v3/";

        private static string GenerateRequestUrl(string request) {
            return ApiEndpoint + request + "?token=" + Secret.Token;
        }

        public static Task<string> GetGroupList() {
            var task = new Task<string>(() => {
                HttpClient client = new HttpClient();

                var groupsResponseTask = client.GetAsync(GroupMeApi.GenerateRequestUrl("groups"));
                var groupsResponseContentTask = groupsResponseTask.Result.Content.ReadAsStringAsync();

                return groupsResponseContentTask.Result;
            });
            task.Start();
            return task;
        }
    }
}