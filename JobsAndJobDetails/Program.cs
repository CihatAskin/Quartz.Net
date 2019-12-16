using System;
using System.Collections.Specialized;

using Quartz;
using Quartz.Impl;

namespace JobsAndJobDetails
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
                      var props = new NameValueCollection() { { "quartz.serializer.type", "binary" } };
            var factory = new StdSchedulerFactory(props);

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            IJobDetail job1 = JobBuilder.Create<HelloJob>().WithIdentity("myJob", "group1")
                                                          .Build();


            IJobDetail job2 = JobBuilder.Create<DumbJob>().WithIdentity("DumbJob", "group1")
                                                          .UsingJobData("jobSays", "Hello World!")
                                                          .UsingJobData("myFloatValue", 3.141f)
                                                          .Build();

            ITrigger trigger1 = TriggerBuilder.Create().WithIdentity("myTrigger1", "group1")
                                                       .StartNow()
                                                       .WithSimpleSchedule(x => x.WithIntervalInSeconds(3)
                                                                                 .RepeatForever())
                                                       .Build();

            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("myTrigger2", "group1")
                                                       .StartNow()
                                                       .WithSimpleSchedule(x => x.WithIntervalInSeconds(6)
                                                                                 .RepeatForever())
                                                       .Build();

            await sched.ScheduleJob(job1, trigger1);
            await sched.ScheduleJob(job2, trigger2);
            Console.ReadLine();
        }
    }
}
