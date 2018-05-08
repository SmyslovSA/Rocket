<<<<<<< HEAD:Rocket.DAL.Common/DbModels/DbPersonalArea/DbUser.cs
﻿using System.Collections.Generic;
using Rocket.DAL.Common.DbModels.DbUserRole;
=======
﻿using Rocket.DAL.Common.DbModels.DbUser.DbAccount;
using Rocket.DAL.Common.DbModels.DbUser.DbPerson;
using System.Collections.Generic;
>>>>>>> develop:Rocket.DAL.Common/DbModels/DbPersonalArea/DbAuthorisedUser.cs

namespace Rocket.DAL.Common.DbModels.DbPersonalArea
{
    /// <summary>
    /// модель хранения данных пользователя
    /// </summary>
    public class DbAuthorisedUser
    {
        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// внешний ключ к таблице DbAccount
        /// </summary>
        public int DbAccountId { get; set; }

        /// <summary>
        /// ссылка на DbAccount
        /// </summary>
        public DbAccount DbAccount { get; set; }

        /// <summary>
        /// внешний ключ к таблице DbPersonality
        /// </summary>
        public int DbPersonalityId { get; set; }

        /// <summary>
        /// ссылка на DbPersonality
        /// </summary>
        public DbPersonality DbPersonality { get; set; }

        /// <summary>
        /// относительный путь от корневой папки приложения к изображению аватара пользователя
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// список e-mail пользователя
        /// </summary>
<<<<<<< HEAD:Rocket.DAL.Common/DbModels/DbPersonalArea/DbUser.cs
        public virtual ICollection<DbEmail> Email { get; set; }
=======
        public ICollection<DbEmail> Email { get; set; }
>>>>>>> develop:Rocket.DAL.Common/DbModels/DbPersonalArea/DbAuthorisedUser.cs

        /// <summary>
        /// коллекция выбранных жанров пользователя
        /// </summary>
        public virtual ICollection<DbGenre> Genres { get; set; }
    }
}
