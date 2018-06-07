namespace Rocket.BL.Common.Services.ReleaseList
{
    public interface ISubscriptionService
    {
        void Subscribe(string userId, int id);

        void Unsubscribe(string userId, int id);
    }
}
