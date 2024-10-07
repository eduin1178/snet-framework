using SNET.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SNET.Framework.Domain.Repositories;

public interface IGenericRepository<T>
{
    Task<T> GetAsync(Guid id);   
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}

