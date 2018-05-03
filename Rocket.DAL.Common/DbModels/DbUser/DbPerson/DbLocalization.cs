namespace Rocket.DAL.Common.DbModels.DbUser.DbPerson
{ 
    /// <summary>
    /// Представляет модель хранения данных о локализации человека пользователя
    /// </summary>
    public class DbLocalization
        {
            /// <summary>
            /// Возвращает или задает уникальный идентификационный номер локализации
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Задает или возвращает гражданство пользователя
            /// </summary>
            public DbCountry Sitizenship { get; set; }
        
            /// <summary>
            /// Задает или возвращает язык пользователя
            /// </summary>
            public DbLanguage Language { get; set; }
        
            /// <summary>
            /// Задает или возвращает человека пользователя
            /// </summary>
            public DbPerson Dbperson { get; set; }
        }
}
