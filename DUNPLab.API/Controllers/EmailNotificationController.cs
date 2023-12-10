using DUNPLab.API.Dto;
using DUNPLab.API.Infrastructure;
using DUNPLab.API.Jobs;
using DUNPLab.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DUNPLab.API.Controllers
{
    public class EmailNotificationController : Controller
    {
        private readonly DunpContext context;
        private readonly IBackgroundJobsService backgroundJobsService;

        public EmailNotificationController(DunpContext context, IBackgroundJobsService backgroundJobsService)
        {
            this.context = context;
            this.backgroundJobsService = backgroundJobsService;
        }

        [HttpPost("add-notification")]
        public async Task<IActionResult> AddNotification([FromBody] EmailNotificationDto notification)
        {
            var emailNotification = new EmailNotification
            {
                Message = notification.Message,
                TimeOfSending = notification.TimeOfSending,
                From = notification.From,
                Subject = notification.Subject
            };

            context.EmailNotifications.Add(emailNotification);
            await context.SaveChangesAsync();
            return Ok(emailNotification);
        }


    }
}
