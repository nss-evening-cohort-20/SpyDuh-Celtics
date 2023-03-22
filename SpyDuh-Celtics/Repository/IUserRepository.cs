using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repository
{
    public interface IUserRepository
    {
        User Add(User user);
        void Delete(int id);
        List<User> GetAll();
        User GetById(int id);
        User GetName(string Name);
        void Update(User user);
    }
}