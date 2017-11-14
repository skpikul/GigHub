using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<NotificationDto> GetNewNotification()
        {
            var userId = User.Identity.GetUserId();
            var notification = _context.UserNotifications
                .Where(un => un.UserId == userId && un.IsRead)
                .Select(un => un.Notification)
                .Include(n => n.Gig.Artist).ToList();

            return notification.Select(Mapper.Map<Notification, NotificationDto>);
        }
    }
}
