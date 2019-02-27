using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupMeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var getGroupsTask = GroupMeApi.GetGroupListAsync();
            Console.WriteLine("Started async group task");
            var groupList = getGroupsTask.Result;

            var tasks = new List<Task>();
            groupList.ForEach(group => {
                NpgSqlApi.AsyncAddOrUpdateGroup(group).Wait();

                var getAllMessagesTask = CollectData.GetAllMessagesAsync(group);
                Console.WriteLine("Started async get all messages task");
                tasks.Add(getAllMessagesTask);
            });

            tasks.ForEach(task => task.Wait());
        }
    }
}
