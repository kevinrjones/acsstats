using System.Collections.Generic;
using System.Linq;
using Domain;
using SimpleTodoRepository;


namespace TodoRepositoryNS
{
    public class TodoRepository : BaseEfRepository<Todo, int>, ITodoRepository
    {
        public IEnumerable<Todo> Get(string searchTerm)
        {
            return string.IsNullOrWhiteSpace(searchTerm) ? Entities.ToList()
                : Entities.Where(t => t.Title.StartsWith(searchTerm)).ToList();
        }

        public Todo GetById(int id)
        {
            return Entities.SingleOrDefault(t => t.Id == id);
        }

        public TodoRepository(TodoDbContext dbContext) : base(dbContext)
        {
        }
    }
}