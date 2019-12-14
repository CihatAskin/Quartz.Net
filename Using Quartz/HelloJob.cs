﻿using System;
using System.Threading.Tasks;

using Quartz;

namespace UsingQuartz
{
    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("HelloJob is executing.");
        }
    }
}
