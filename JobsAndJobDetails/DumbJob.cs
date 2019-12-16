using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quartz;

namespace JobsAndJobDetails
{
    public class DumbJob : IJob
    {
        public string JobSays { private get; set; }
        public float FloatValue { private get; set; }

        public async Task Execute(IJobExecutionContext context)
        {

            JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.MergedJobDataMap;  // Note the difference from HelloJob example

            JobSays = dataMap.GetString("jobSays");
            FloatValue = dataMap.GetFloat("myFloatValue");

            //var state = (IList<DateTimeOffset>)dataMap["myStateData"];
            //state.Add(DateTimeOffset.UtcNow);

            await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + JobSays + ", and val is: " + FloatValue);
        }
    }
}
