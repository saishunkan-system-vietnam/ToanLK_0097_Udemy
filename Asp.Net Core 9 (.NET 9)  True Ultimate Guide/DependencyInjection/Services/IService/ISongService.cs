
using DependencyInjection.model;

namespace DependencyInjection.Services.IService
{
    public interface ISongService
    {
        public IEnumerable<Song> GetSong();
    }
}
