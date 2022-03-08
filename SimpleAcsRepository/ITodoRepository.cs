using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Repository;


namespace TodoRepositoryNS
{
    public interface ITodoRepository : IRepository<Todo>
    {
        IEnumerable<Todo> Get(string searchTerm);

        Todo GetById(int id);
    }
}
