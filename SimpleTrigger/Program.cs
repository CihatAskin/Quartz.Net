using System;
using System.Collections.Specialized;

using Quartz;
using Quartz.Impl;
using Quartz.Impl.Calendar;

namespace SimpleTrigger
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var props = new NameValueCollection()
            {
                { "quartz.serializer.type", "binary" }
            };

            var factory = new StdSchedulerFactory(props);

            IScheduler sched = await factory.GetScheduler();
            await sched.Start();

            var cal = new HolidayCalendar();
            cal.AddExcludedDate(DateTime.Now.AddDays(-1));
            await sched.AddCalendar("myHolidays", cal, false, false);

            IJobDetail job = JobBuilder.Create<DumbJob>().WithIdentity("myJob", "group1")
                                                         .UsingJobData("jobSays", "Hello World!")
                                                         .UsingJobData("myFloatValue", 3.141f)
                                                         .Build();

            ITrigger trigger = TriggerBuilder.Create().WithIdentity("myTrigger")
                                                      .WithSchedule(CronScheduleBuilder.CronSchedule("0-10 * * ? * * *")) // first 10 second every minute
                                                      .ModifiedByCalendar("myHolidays") // but not on holidays
                                                      .Build();

            ITrigger trigger1 = TriggerBuilder.Create().WithIdentity("trigger1", "group1")
                                                       .WithCronSchedule("0-10 * * ? * * *") // first 10 second every minute
                                                       .ForJob("myJob", "group1")
                                                       .Build();

            ITrigger trigger2 = TriggerBuilder.Create().WithIdentity("myTrigger2")
                                                       .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(16, 30)) // execute job daily at 16.30
                                                       .ModifiedByCalendar("myHolidays") // but not on holidays
                                                       .Build();

            //Build a trigger that will fire on Wednesdays at 10:42 am, in a TimeZone other than the system’s default:
            ITrigger trigger21 = TriggerBuilder.Create().WithIdentity("trigger21", "group1")
                                                        .WithCronSchedule("0 42 10 ? * WED", x => x
                                                                          .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                                                        .Build();

            ITrigger trigger22 = TriggerBuilder.Create().WithIdentity("trigger3", "group1")
                                                        .WithSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Wednesday, 10, 42)
                                                                                         .InTimeZone(TimeZoneInfo.FindSystemTimeZoneById("Central America Standard Time")))
                                                        .Build();

            //SimpleTrigger : once at a specific moment in time, or at a specific moment in time followed by repeats at a specific interval. 

            //Build a trigger for a specific moment in time, with no repeats:
            ISimpleTrigger trigger3 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("myTrigger3", "group1")
                                                                    .StartAt(DateTimeOffset.UtcNow.AddSeconds(5))
                                                                    .ForJob("myJob", "group1") // identify job with name, group strings
                                                                    .Build();

            //Build a trigger for a specific moment in time, then repeating every ten seconds ten times:
            ISimpleTrigger trigger4 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("myTrigger4", "group1")
                                                                    .StartAt(DateTimeOffset.UtcNow.AddSeconds(5)) // if a start time is not given (if this line were omitted), "now" is implied
                                                                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(10)
                                                                                              .WithRepeatCount(10)) // note that 10 repeats will give a total of 11 firings
                                                                    .ForJob("myJob", "group1")
                                                                    .Build();

            //Build a trigger that will fire once, five minutes in the future:
            ISimpleTrigger trigger5 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("myTrigger5", "group1")
                                                                    .StartAt(DateBuilder.FutureDate(5, IntervalUnit.Second)) // use DateBuilder to create a date in the future
                                                                    .ForJob("myJob", "group1")
                                                                    .Build();

            //Build a trigger that will fire now, then repeat every five minutes, until the hour 17:10:
            ISimpleTrigger trigger6 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("myTrigger6", "group1")
                                                                    .WithSimpleSchedule(x => x.WithIntervalInSeconds(5)
                                                                                              .RepeatForever())
                                                                    .EndAt(DateBuilder.DateOf(22, 10, 0))
                                                                    .Build();

            //Build a trigger that will fire at the top of the next hour, then repeat every 2 hours, forever:
            ISimpleTrigger trigger7 = (ISimpleTrigger)TriggerBuilder.Create().WithIdentity("myTrigger7") // because group is not specified, "trigger8" will be in the default group
                                                                    .StartAt(DateBuilder.EvenHourDate(null)) // get the next even-hour (minutes and seconds zero ("00:00"))
                                                                    .WithSimpleSchedule(x => x.WithIntervalInHours(2)
                                                                                              .RepeatForever())
                                                                    // note that in this example, 'forJob(..)' is not called 
                                                                    //  - which is valid if the trigger is passed to the scheduler along with the job  
                                                                    .Build();

            await sched.ScheduleJob(job, trigger1);

            Console.ReadLine();
        }
    }
}
