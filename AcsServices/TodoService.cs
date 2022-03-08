using System.Collections.Generic;
using Domain;
using TodoRepositoryNS;

namespace TodoServices
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public IEnumerable<Todo> Get(string searchTerm)
        {
            return _todoRepository.Get(searchTerm);
        }

        public Todo GetById(int id)
        {
            return _todoRepository.GetById(id);
        }

        public void Add(Todo todo)
        {
            _todoRepository.Create(todo);
        }

        public void Update(Todo todo)
        {
            _todoRepository.Update(todo);
        }
    }
}
