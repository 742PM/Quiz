using System;
using Infrastructure.DDD;

namespace Application.Repositories
{
    public class Editor : Entity
    {
        public Editor(Guid id, string passwordHash, string username, string[] rights) : base(id)
        {
            PasswordHash = passwordHash;
            Username = username;
            Rights = rights;
        }

        public string PasswordHash { get; }
        public string Username { get; }
        public string[] Rights { get; }
    }
}