using System;
using System.Collections.Specialized;

using Quartz;
using Quartz.Impl;

namespace UsingQuartz
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            // construct a scheduler factory
            var props = new NameValueCollection() { { "quartz.serializer.type", "binary" } };
            var factory = new StdSchedulerFactory(props);

            // get a scheduler
            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<HelloJob>().WithIdentity("myJob", "group1")
                                                          .Build();

            // Trigger the job to run now, and then every 3 seconds
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger", "group1")
                                                      .StartNow()
                                                      .WithSimpleSchedule(x => x.WithIntervalInSeconds(3)
                                                                                .RepeatForever())
                                                      .Build();

            await sched.ScheduleJob(job, trigger);
            Console.ReadLine();
        }
    }
}
