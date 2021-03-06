﻿using System;
using System.Collections.Generic;
using Application.Repositories;
using Application.Repositories.Entities;

namespace Tests.Mocks
{
    public class TestUserRepository : IUserRepository
    {
        private readonly Dictionary<Guid, UserEntity> users = new Dictionary<Guid, UserEntity>();

        public UserEntity Insert(UserEntity user)
        {
            users[user.Id] = user;
            return user;
        }

        public UserEntity FindById(Guid id) => users.TryGetValue(id, out var user) ? user : null;

        public void Update(UserEntity user)
        {
            users[user.Id] = user;
        }

        public UserEntity UpdateOrInsert(UserEntity user)
        {
            Update(user);
            return user;
        }

        public void Delete(Guid id)
        {
            users.Remove(id);
        }
    }
}
