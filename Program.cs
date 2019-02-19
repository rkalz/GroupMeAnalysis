using System;

namespace groupme_analysis
{
    class Program
    {
        static void Main(string[] args)
        {
            var getGroupsTask = GroupMeApi.GetGroupList();
            Console.WriteLine("Started async group task");

            Console.WriteLine(getGroupsTask.Result);
        }
    }
}
