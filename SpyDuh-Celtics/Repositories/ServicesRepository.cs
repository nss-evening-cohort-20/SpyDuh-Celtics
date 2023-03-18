using Microsoft.Extensions.Configuration;

namespace SpyDuh_Celtics.Repositories
{
    public class ServicesRepository : BaseRepository
    {
        /// <summary>
        ///  When new RoomRepository is instantiated, pass the connection string along to the BaseRepository
        /// </summary>
        public ServicesRepository(IConfiguration configuration) : base(configuration) { }

    }
}
