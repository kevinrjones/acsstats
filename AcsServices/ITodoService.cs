using System.Collections.Generic;
using Domain;

namespace TodoServices
{
    public interface ITodoService
    {
        IEnumerable<Todo> Get(string searchTerm);

        Todo GetById(int id);

        void Add(Todo todo);
        void Update(Todo todo);
    }
}
