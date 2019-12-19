using System;
using System.Threading;
using System.Threading.Tasks;

using Quartz;

namespace Listeners
{
    public class MyTriggerListener : ITriggerListener
    {
        public string Name
        {
            get { return nameof(MyJobListener); }
        }

        public Task TriggerComplete(ITrigger trigger, IJobExecutionContext context, SchedulerInstruction triggerInstructionCode, CancellationToken cancellationToken = default)
        {
            var jobKey = context.JobDetail.Key;
            var jobTrigger = trigger.Key;

            Task task = Task.Run(() => Console.WriteLine("{0} Completed for {1}", jobTrigger, jobKey));

            return task;
        }

        public Task TriggerFired(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            var jobKey = context.JobDetail.Key;
            var jobTrigger = trigger.Key;

            Task task = Task.Run(() => Console.WriteLine("{0} Fired for {1}", jobTrigger, jobKey));

            return task;
        }

        public Task TriggerMisfired(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            var jobTrigger = trigger.Key;
            Task task = Task.Run(() => Console.WriteLine("Trigger Misfired for {0}", jobTrigger));

            return task;
        }

        public Task<bool> VetoJobExecution(ITrigger trigger, IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            var jobKey = context.JobDetail.Key;
            var jobTrigger = trigger.Key;

            Task task = Task.Run(() => Console.WriteLine("{0} Vetoed for {1}", jobTrigger, jobKey));

            return Task.FromResult(false);
        }
    }
}
