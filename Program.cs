using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupMeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var groupList = GroupMeApi.GetGroupListAsync().Result;

            var tasks = new List<Task>();
            groupList.ForEach(group => {
                NpgSqlApi.AsyncAddOrUpdateGroup(group).Wait();

                //var getAllMessagesTask = CollectData.GetAllMessagesAsync(group);
                //tasks.Add(getAllMessagesTask);
            });

            tasks.ForEach(task => task.Wait());
        }
    }
}
