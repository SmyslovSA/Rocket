namespace Rocket.DAL.Common.DbModels.DbUser.DbPerson
{
    /// <summary>
    /// Представляет модель хранениея данных адреса для сведений о человеке пользователя
    /// </summary>
    public class DbAddress
    {
        /// <summary>
        /// Задает или возвращает уникальный идентификационный номер адреса
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Задает или возвращает почтовый индекс
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// Возвращает или задает страну
        /// </summary>
        public DbCountry Country { get; set; }

        /// <summary>
        /// Возвращает или задает город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Возвращает или задает номер строения (дома)
        /// </summary>
        public string Building { get; set; }

        /// <summary>
        /// Возвращает или задает номер корпуса
        /// </summary>
        public string BuildingBlock { get; set; }

        /// <summary>
        /// Возвращает или задает номер квартиры
        /// </summary>
        public string Flat { get; set; }

        /// <summary>
        /// Возвращает или задает контактные данные, к которым относится адрес
        /// </summary>
        public DbCommunication DbCommunication { get; set; }
    }
}
