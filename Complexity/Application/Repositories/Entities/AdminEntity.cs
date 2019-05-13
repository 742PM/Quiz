using System;
using Infrastructure.DDD;

namespace Application.Repositories.Entities
{
    public class AdminEntity : Entity
    {
        public AdminEntity(Guid id) : base(id)
        {
        }
        
        public AdminEntity(Guid id, string passwordHash) : base(id)
        {
            PasswordHash = passwordHash;
        }
        
        public string PasswordHash { get; }
    }
}