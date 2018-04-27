namespace Rocket.BL.Common.Models.ReleaseList
{
    /// <summary>
    /// Выделен в отдельных тип, ибо может применяться
    /// как в адресе пользователя, так и в типе, где 
    /// он отражает национальность, а также язык, на
    /// котором говорит пользователь.
    /// </summary>
    public class Country
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
