using SpyDuh_Celtics.Models;

namespace SpyDuh_Celtics.Repositories
{
    public interface ISkillsRepository
    {
        bool Delete(int id);
        List<Skills> GetById(int id);
        List<Skills> GetAll();
        bool Insert(SkillInsert skill);
        bool Update(SkillInsert skill);
    }
}