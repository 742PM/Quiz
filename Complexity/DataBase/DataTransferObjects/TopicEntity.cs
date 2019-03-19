using System;
using System.Threading.Tasks;

namespace DataBase
{
    public class TopicEntity
    {
        public Guid TopicId { get; set; }
        
        public string Name { get; set; }
        public Task[] Tasks { get; set; }
    }
}