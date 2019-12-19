using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Quartz;

namespace Listeners
{
    public class MySchedulerListener : ISchedulerListener
    {
        public Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() =>
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("JobAdded");
                Console.WriteLine(Environment.NewLine);
            });

            return task;
        }

        public Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobDeleted"));

            return task;
        }

        public Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobInterrupted"));

            return task;
        }

        public Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobPaused"));

            return task;
        }

        public Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobResumed"));

            return task;
        }

        public Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobScheduled"));

            return task;
        }

        public Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobsPaused"));

            return task;
        }

        public Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobsResumed"));

            return task;
        }

        public Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("JobUnscheduled"));

            return task;
        }

        public Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerError"));

            return task;
        }

        public Task SchedulerInStandbyMode(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerInStandbyMode"));

            return task;
        }

        public Task SchedulerShutdown(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerShutdown"));

            return task;
        }

        public Task SchedulerShuttingdown(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerShuttingdown"));

            return task;
        }

        public Task SchedulerStarted(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerStarted"));

            return task;
        }

        public Task SchedulerStarting(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulerStarting"));

            return task;
        }

        public Task SchedulingDataCleared(CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("SchedulingDataCleared"));

            return task;
        }

        public Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("TriggerFinalized"));

            return task;
        }

        public Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("TriggerPaused"));

            return task;
        }

        public Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("TriggerResumed"));

            return task;
        }

        public Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("TriggersPaused"));

            return task;
        }

        public Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default)
        {
            Task task = Task.Run(() => Console.WriteLine("TriggersResumed"));

            return task;
        }
    }
}
