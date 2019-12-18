using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrigger
{
   public class DumbJob :IJob
    {
        public string JobSays { private get; set; }
        public float FloatValue { private get; set; }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobKey key = context.JobDetail.Key;
                JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from the previous example

                JobSays = dataMap.GetString("jobSays");
                FloatValue = dataMap.GetFloat("myFloatValue");
               
                await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + JobSays + ", and val is: " + FloatValue);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
