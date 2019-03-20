using System;
using System.Threading.Tasks;

namespace DataBase
{
    public class UserEntity
    {
        public UserEntity()
        {
            Id = Guid.Empty;
        }

        public UserEntity(Guid id)
        {
            Id = id;
        }

        public UserEntity(Guid id, Progress progress)
        {
            Id = id;
            Progress = progress;
        }

        public Guid Id
        {
            get;
            // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local For MongoDB
            private set;
        }
        
        public TaskEntity CurrentTask { get; set; }

        public Progress Progress { get; set; }
    }
}