using Application.Repositories.Entities;

namespace Application.Repositories
{
    public interface IAdminRepository
    {
        AdminEntity Insert(AdminEntity adminEntity);
        AdminEntity FindByHash(string hash);
        void Delete(string hash);
        void Update(AdminEntity adminEntity);
    }
}