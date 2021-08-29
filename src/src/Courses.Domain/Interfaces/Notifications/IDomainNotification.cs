using Courses.Domain.Models.Notification;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Courses.Domain.Interfaces.Notifications
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotification(string key, string message);
        void AddNotifications(IEnumerable<NotificationMessage> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}
