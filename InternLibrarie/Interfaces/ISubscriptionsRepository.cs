using InternshipLibrarie.Models;

namespace InternshipLibrarie.Interfaces
{
    public interface ISubscriptionsRepository
    {
        Task RenewSubscriptionAsync(int userId);

        Subscriptions GetSubscriptionByUserId(int userId);
    }
}
