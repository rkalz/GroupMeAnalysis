using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GroupMeAnalysis {
    static class CollectData {
        public static Task<List<Message>> GetAllMessagesAsync(Group group) {
            var task = new Task<List<Message>>(() => {
                var timer = new Stopwatch();

                timer.Start();
                var fetchTask = GroupMeApi.GetMessagesAsync(group.Id);
                var messages = fetchTask.Result;

                while (messages.Count < group.MessageInfo.Count) {
                    Console.WriteLine($"Downloaded {messages.Count} messages so far");
                    fetchTask = GroupMeApi.GetMessagesAsync(group.Id, messages.Last().Id);
                    messages.AddRange(fetchTask.Result);
                }
                timer.Stop();

                Console.WriteLine($"Downloaded {messages.Count} messages in {timer.ElapsedMilliseconds} ms");
                return messages;
            });

            task.Start();
            return task;
        }
    }
}