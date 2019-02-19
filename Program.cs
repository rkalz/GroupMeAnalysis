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
        }
    }
}
