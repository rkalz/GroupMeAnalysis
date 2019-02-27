using System;

namespace GroupMeAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var getGroupsTask = GroupMeApi.GetGroupListAsync();
            Console.WriteLine("Started async group task");
            var groupList = getGroupsTask.Result;

            var group = groupList.Find(g => g.Id.Equals("19224977"));
            NpgSqlApi.AsyncAddOrUpdateGroup(group).Wait();

            var getAllMessagesTask = CollectData.GetAllMessagesAsync(group);
            Console.WriteLine("Started async get all messages task");
            var messages = getAllMessagesTask.Result;
        }
    }
}
