using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using PasswordEncryptor.Domain.Entities;

namespace PasswordEncryptor.Application.Abstraction.IRepository
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task AddAsync(T model);

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T,bool>> expression);

        Task DeleteAsync(Guid id);

        Task UpdateAsync(T model);

        // dapper method
        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object? param = null, CommandType commandType=CommandType.Text, IDbTransaction? transaction=null);
    }
}
