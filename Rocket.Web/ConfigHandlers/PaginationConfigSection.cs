using System.Configuration;

namespace Rocket.Web.ConfigHandlers
{
    public class PaginationConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("pageSize", DefaultValue = 10)]
        public int PageSize
        {
            get => (int)this["pageSize"];
            set => this["pageSize"] = value;
        }
    }
}