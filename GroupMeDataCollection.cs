using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupMeAnalysis {
    static class CollectData {
        public static Task GetAllMessagesAsync(Group group) {
            var task = new Task(() => {
                var startId = NpgSqlApi.GetNewestGroupMessageAsync(group).Result;
                Task newMessagesTask = null;
                if (startId != null && startId != group.MessageInfo.LastMessageId) {
                    // Get all messages after startId
                    newMessagesTask = new Task (() => {
                        var newMsgs = GroupMeApi.GetMessagesAfterIdAsync(group.Id, startId).Result;
                        while (newMsgs.Count != 0) {
                            NpgSqlApi.AddMessagesToDatabaseAsync(group, newMsgs);
                            var lastMessageId = newMsgs.Last().Id;
                            newMsgs = GroupMeApi.GetMessagesAfterIdAsync(group.Id, lastMessageId).Result;
                        }
                    });
                    newMessagesTask.Start();
                }

                var oldId = NpgSqlApi.GetOldestGroupMessageAsync(group).Result;
                if (oldId == null) oldId = "";
                var oldMsgs = GroupMeApi.GetMessagesBeforeIdAsync(group.Id, oldId).Result;

                while (oldMsgs.Count != 0) {
                    NpgSqlApi.AddMessagesToDatabaseAsync(group, oldMsgs);
                    oldId = oldMsgs.Last().Id;
                    oldMsgs = GroupMeApi.GetMessagesBeforeIdAsync(group.Id, oldId).Result;
                }

                if (newMessagesTask != null) newMessagesTask.Wait();
            });

            task.Start();
            return task;
        }
    }
}