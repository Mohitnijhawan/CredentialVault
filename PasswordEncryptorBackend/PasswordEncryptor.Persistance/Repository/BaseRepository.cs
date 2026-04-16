using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PasswordEncryptor.Application.Abstraction.IRepository;
using PasswordEncryptor.Domain.Entities;
using PasswordEncryptor.Persistance.Data;

namespace PasswordEncryptor.Persistance.Repository
{
    public class BaseRepository<T>(PasswordEncryptorDbContext context) : IBaseRepository<T> where T : BaseEntity, new()
    {
        public async Task AddAsync(T model)
        {
            await context.Set<T>().AddAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            await Task.Run(() => context.Remove(entity!));
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
        {
            return (await context.Set<T>().FirstOrDefaultAsync(expression))!;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return (await context.Set<T>().FindAsync(id))!;
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(string sql, object? param = null, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
        {
            using SqlConnection connection = new(context.Database.GetConnectionString());
            return await connection.QueryAsync<TEntity>(sql, param, transaction, null, commandType);
        }

        public async Task UpdateAsync(T model)
        {
            await Task.Run(() => context.Set<T>().Update(model));
        }
    }
}
