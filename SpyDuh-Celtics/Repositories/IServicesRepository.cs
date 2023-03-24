using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repositories
{
    public interface IServicesRepository
    {
        IList<Services> GetAll();
        Services GetById(int id);
        void Add(Services service);
        void Update(Services service);
        void Remove(int id);
    }
}
