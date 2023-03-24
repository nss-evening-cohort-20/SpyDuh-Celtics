using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repositories
{
    public interface IRelationshipRepository
    {
        void AddFriend(NewRelationship relationship);
        void AddFoe(NewRelationship relationship);
        void Delete(int id);
        List<Relationship> GetAllFoesById(int id);
        List<Relationship> GetAllFriendsById(int id);
    }
}