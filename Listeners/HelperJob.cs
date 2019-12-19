using System;
using System.Threading.Tasks;

using Quartz;

namespace Listeners
{
    public class HelperJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync("-----Helper Job-----");
        }
    }
}
