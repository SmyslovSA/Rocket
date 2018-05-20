using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.Notifications.Notifications
{
    public class PushNotifications
    {
        public async Task NotifyAsync()
        {
            try
            {

            }
            catch (Exception e)
            {
                //todo логирование
                Console.WriteLine(e);
                throw e;
            }
        }
    }
}
