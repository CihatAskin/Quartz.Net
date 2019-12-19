using System;
using System.Threading;
using System.Threading.Tasks;

using Quartz;

namespace Listeners
{
    public class MyJobListener : IJobListener
    {
        public string Name
        {
            get { return nameof(MyJobListener); }
        }

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            JobKey jobKey = context.JobDetail.Key;
            Task task = Task.Run(() => Console.WriteLine("\n {0} vetoed at {1} \n", jobKey, DateTime.Now.Second));

            return task;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken)
        {
            JobKey jobKey = context.JobDetail.Key;
            Task task = Task.Run(() => Console.WriteLine("\n{0} started at {1}", jobKey, DateTime.Now.Second));

            return task;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken)
        {
            JobKey jobKey = context.JobDetail.Key;
            Task task = Task.Run(() => Console.WriteLine("{0} finished at {1} ", jobKey, DateTime.Now.Second));

            return task;
        }
    }
}
