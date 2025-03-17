using AutoMapper;
using Business.Abstracts;
using Business.Requests.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class DomainNotificationWorker : IHostedService, IDisposable
    {
        private readonly IServiceProvider _serviceProvider;
        private System.Timers.Timer _taskTimer;

        public DomainNotificationWorker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var firstRunTime = DateTime.Today.AddSeconds(30);

            //if (now > firstRunTime)
            //{
            //    firstRunTime = firstRunTime.AddDays(1);
            //}

            //var timeToWait = firstRunTime - now;

            //Console.WriteLine($"Mail gönderimine {timeToWait.Hours} saat ve {timeToWait.Minutes} dakika kaldı.");

            //_taskTimer = new System.Timers.Timer(timeToWait.TotalMilliseconds);
            _taskTimer = new System.Timers.Timer(TimeSpan.FromSeconds(30).TotalMilliseconds);
            _taskTimer.Elapsed += async (sender, args) => await ExecuteTaskAsync();
            _taskTimer.AutoReset = true;
            _taskTimer.Start();

            return Task.CompletedTask;
        }

        //private async Task ScheduleTaskAsync()
        //{
        //    await ExecuteTaskAsync();

        //    _taskTimer?.Dispose();
        //    _taskTimer = new System.Timers.Timer(TimeSpan.FromDays(1).TotalMilliseconds); 
        //    _taskTimer.Elapsed += async (sender, args) => await ExecuteTaskAsync();
        //    _taskTimer.AutoReset = true;
        //    _taskTimer.Start();
        //}

        private async Task ExecuteTaskAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var domainService = scope.ServiceProvider.GetRequiredService<IDomainService>();
                var request = new GetDomainListRequest();
                await domainService.GetListMin90Days(request);
            }

            Console.WriteLine("Mail gönderildi.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _taskTimer?.Stop();
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _taskTimer?.Dispose();
        }
    }
}
