using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        void Delete(int id);
        User Get(int id);
        User Get(string Name);
        List<User> GetAll();
        void Update(User user);
    }
}