using System;
using DataBase.DatabaseEntities;

namespace DataBase
{
    public interface IUserRepository
    {
        UserEntity Insert(UserEntity user);
        UserEntity FindById(Guid id);

        void Update(UserEntity user);
        UserEntity UpdateOrInsert(UserEntity user);
        void Delete(Guid id);
    }
}
