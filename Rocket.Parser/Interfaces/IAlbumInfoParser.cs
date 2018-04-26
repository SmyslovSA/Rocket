using System.Threading.Tasks;

namespace Rocket.Parser.Interfaces
{
    public interface IAlbumInfoParser
    {
        Task<int> ParseAsync();
    }
}