using System;
using System.Threading.Tasks;

using Quartz;

namespace Listeners
{
    public class DumbJob : IJob
    {
        public string JobSays { private get; set; }
        public float FloatValue { private get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.MergedJobDataMap;

                JobSays = dataMap.GetString("jobSays");
                FloatValue = dataMap.GetFloat("myFloatValue");

                await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + JobSays + ", and val is: " + FloatValue);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
