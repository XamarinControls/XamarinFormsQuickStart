using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ISQLiteRepository
    {
        Task<Unit> Create<T>(string name, T obj);
        Task<T> Get<T>(string name);
        Task<IEnumerable<T>> GetAll<T>();
    }
}
