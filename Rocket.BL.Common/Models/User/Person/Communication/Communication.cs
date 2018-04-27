using System.Collections.Generic;

namespace Rocket.BL.Common.Models.User
{
    /// Средства коммуникации с человеком и, в конечном счету,
    /// с пользователем. Все, что только может понадобиться для 
    /// коммуникации собрано здесь.
    /// </summary>
    public class Communication
    {
        // Как обращаться к пользователю при переписке,
        // Email-ах и так далее. Определен тип 'HowToCall'
        // путем перечисления.
        public HowToCall? HowToCall { get; set; }
        
        // Список телефонных номеров с кодами
        public List<IPhone> Phones { get; set; }

        // Каналы связи: мессенджеры, соцсети.
        public List<Communicator> Communicators { get; set; }

        // Список адресов электронной почты
        public List<string> EMailAddresses { get; set; }

        // Почтовый адрес. Ссылается на общий для проекта 
        // тип 'Address'.
        public IAddress MailAddress { get; set; }
    }
}
