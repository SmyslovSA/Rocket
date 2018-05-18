using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocket.BL.Services.EmailNotificationService
{
    class HtmlStringBuilder
    {

        public string HtmlBody;

        public string CreateBody(/*todo забить сюда шаблон*/)
        {
            string[] bodyArray = new string[0]; //todo Забрать из базы данных
            string HtmlBody = string.Empty;
            foreach(string s in bodyArray)
            {
                HtmlBody = HtmlBody + s;
            }
            return HtmlBody;
        }
    }
}
