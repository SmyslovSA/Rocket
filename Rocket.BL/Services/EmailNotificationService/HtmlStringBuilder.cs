namespace Rocket.BL.Services.EmailNotificationService
{
    public class HtmlStringBuilder
    {
        public string CreateBody(/*todo забить сюда шаблон*/)
        {
            string[] bodyArray = new string[0]; //todo Забрать из базы данных
            string htmlBody = string.Empty;
            foreach (string s in bodyArray)
            {
                htmlBody = htmlBody + s;
            }

            return htmlBody;
        }
    }
}
