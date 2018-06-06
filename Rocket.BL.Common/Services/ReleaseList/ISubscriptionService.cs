using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Common.Services.ReleaseList
{
    public interface ISubscriptionService
    {
        void Subscribe(string userId, int id);
        void Unsubscribe(string userId, int id);
    }
}
