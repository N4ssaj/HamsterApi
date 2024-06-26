﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.Core.Stores.Base;

public interface IBaseStore<T>
{
    public Task<string> Create(T item);

    public Task<T?> Read(string id);

    public Task<List<T>> ReadAll();

    public Task<bool> Delete(string id );

    public Task<List<T>> ReadByIds(IEnumerable<string> ids);
}
