using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Quartz;

namespace JobsAndJobDetails
{
	public class HelloJob : IJob
	{
		public async Task Execute(IJobExecutionContext context)
		{
			JobKey key = context.JobDetail.Key;
			JobDataMap dataMap = context.JobDetail.JobDataMap;

			string jobSays = dataMap.GetString("jobSays");

			//IList<DateTimeOffset> state = (IList<DateTimeOffset>)dataMap["myStateData"];
			//state.Add(DateTimeOffset.UtcNow);

			await Console.Error.WriteLineAsync("Instance " + key + " of DumbJob says: " + jobSays);
		}
	}
}
