using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.Core.Stores.Base;

public interface IBaseStore<T>
{
    public Task<Guid> Create(T item);

    public Task<T> Read(string id );

    public Task<List<T>> ReadAll();

    public Task Delete(string id );
}
