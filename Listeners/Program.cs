using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;

namespace Listeners
{
    public class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var myJobListener = new MyJobListener();
            var myTriggerListener = new MyTriggerListener();
            var mySchedulerListener = new MySchedulerListener();

            NameValueCollection props = new NameValueCollection()
            {
                { "quartz.serializer.type", "binary" }
            };

            StdSchedulerFactory factory = new StdSchedulerFactory(props);

            // await InstanceForOneJob(myJobListener, factory);
            // await InstanceForJobsInGroup(myJobListener, factory);
            // await InstanceForJobsInDifferentGroups(myJobListener, factory);
            await InstanceForOneTrigger(myTriggerListener, factory);

            //Adding a SchedulerListener:
            //scheduler.ListenerManager.AddSchedulerListener(mySchedulerListener);

            //Removing a SchedulerListener:
            //scheduler.ListenerManager.RemoveSchedulerListener(mySchedListener);

            Console.ReadLine();
        }

        private static async Task InstanceForOneJob(MyJobListener myJobListener, StdSchedulerFactory factory)
        {
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myJob")
                                                         .UsingJobData("jobSays", "Hello World!")
                                                         .UsingJobData("myFloatValue", 3.141f)
                                                         .Build();

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(2)
                                                                                                       .WithRepeatCount(10))
                                                                             .Build();

            //Adding a JobListener that is interested in a particular job:
            sched.ListenerManager.AddJobListener(myJobListener, KeyMatcher<JobKey>.KeyEquals(new JobKey("myJob")));

            await sched.ScheduleJob(job, trigger);
        }

        private static async Task InstanceForJobsInGroup(MyJobListener myJobListener, StdSchedulerFactory factory)
        {
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("firstJob", "group1")
                                                         .UsingJobData("jobSays", "first")
                                                         .UsingJobData("myFloatValue", 3.141f)
                                                         .Build();

            IJobDetail job2 = JobBuilder.Create<HelloJob>().WithIdentity("secondJob", "group1")
                                                           .Build();

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger", "group1")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(2)
                                                                                                       .WithRepeatCount(10))
                                                                             .Build();

            ISimpleTrigger trigger2 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger2", "group1")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(4)
                                                                                                       .WithRepeatCount(10))
                                                                             .Build();

            //Adding a JobListener that is interested in all jobs of a particular group:
            sched.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.GroupEquals("group1"));

            await sched.ScheduleJob(job, trigger2);
            await sched.ScheduleJob(job2, trigger);
        }

        private static async Task InstanceForJobsInDifferentGroups(MyJobListener myJobListener, StdSchedulerFactory factory)
        {
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("firstJob", "firstGroup")
                                                         .UsingJobData("jobSays", "first")
                                                         .UsingJobData("myFloatValue", 3.141f)
                                                         .Build();

            IJobDetail job2 = JobBuilder.Create<HelloJob>().WithIdentity("secondJob", "firstGroup")
                                                           .Build();

            IJobDetail job3 = JobBuilder.Create<HelperJob>().WithIdentity("firstJob", "secondGroup")
                                                           .Build();

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(1)
                                                                                                       .WithRepeatCount(3))
                                                                             .Build();

            ISimpleTrigger trigger2 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger2")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(3)
                                                                                                       .WithRepeatCount(3))
                                                                             .Build();

            ISimpleTrigger trigger3 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger3")
                                                                            .StartNow()
                                                                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
                                                                                                      .WithRepeatCount(3))
                                                                            .Build();

            //Adding a JobListener that is interested in all jobs of a particular group:
            //sched.ListenerManager.AddJobListener(myJobListener, OrMatcher<JobKey>.Or(GroupMatcher<JobKey>.GroupEquals("firstGroup"),
            //GroupMatcher<JobKey>.GroupEquals("secondGroup")));

            //Adding a JobListener that is interested in all jobs:
            sched.ListenerManager.AddJobListener(myJobListener, GroupMatcher<JobKey>.AnyGroup());

            await sched.ScheduleJob(job, trigger);
            await sched.ScheduleJob(job2, trigger2);
            await sched.ScheduleJob(job3, trigger3);
        }

        private static async Task InstanceForOneTrigger(MyTriggerListener myTriggerListener, StdSchedulerFactory factory)
        {
            var mySchedulerListener = new MySchedulerListener();

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myJob")
                                                         .UsingJobData("jobSays", "Hello World!")
                                                         .UsingJobData("myFloatValue", 3.141f)
                                                         .Build();

            ISimpleTrigger trigger = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("trigger")
                                                                             .StartNow()
                                                                             .WithSimpleSchedule(x => x.WithIntervalInSeconds(4)
                                                                                                       .WithRepeatCount(4))
                                                                             .Build();

            //Adding a TriggerListener that is interested in a particular trigger:
            sched.ListenerManager.AddTriggerListener(myTriggerListener, KeyMatcher<TriggerKey>.KeyEquals(new TriggerKey("trigger")));
            sched.ListenerManager.AddSchedulerListener(mySchedulerListener);
            await sched.ScheduleJob(job, trigger);
        }
    }
}
