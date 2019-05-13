using System;

namespace QuizBotCore.Database
{
    public interface IUserRepository
    {
        UserEntity Insert(UserEntity user);
        UserEntity FindById(Guid id);

        UserEntity FindByTelegramId(long telegramId);

        void Update(UserEntity user);
        UserEntity UpdateOrInsert(UserEntity user);
        void Delete(Guid id);
    }
}
