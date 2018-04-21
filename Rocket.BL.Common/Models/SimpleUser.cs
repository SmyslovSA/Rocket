using System;
using System.Collections.Generic;

namespace Rocket.BL.Common.Models
{
    //авторизованный пользователь с обычными правами
    public class SimpleUser : User
    {
        public string Avatar { get; set; }
        public ICollection<string> Email { get; set; }
        public PersonalizedTape Personalized { get; set; }
    }
}
