using TodoApi.Models;

namespace TodoApi.Repositories
{
    public interface ITodoRepository
    {
        void Create(TodoItem item);
        IEnumerable<TodoItem> Get();
        TodoItem Get(string key);
        TodoItem Delete(string key);
        void Update(TodoItem item);   }
}