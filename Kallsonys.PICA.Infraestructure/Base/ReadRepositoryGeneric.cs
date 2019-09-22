using Kallsonys.PICA.ContractsRepositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kallsonys.PICA.CrossCutting.Configuration;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;

namespace Kallsonys.PICA.Infraestructure.Base
{
    public abstract class ReadRepositoryGeneric<TEntity, TKey> : IReadRepository<TEntity, TKey> where TEntity : class, new() where TKey : struct
    {
        /// <summary> The context </summary>
        internal DbContext context;

        /// <summary> The database set </summary>
        /// <value> The database set. </value>
        internal DbSet<TEntity> DbSet { get { return context.Set<TEntity>(); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadRepositoryGeneric{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public ReadRepositoryGeneric(DbContext context)
        {
            this.context = context;
        }

        /// <summary> Counts the asynchronous. </summary>
        /// <param name="predicate">         The predicate. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public async virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            try
            {
                return await DbSet.Where(predicate).CountAsync();
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorCountAsyc, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        /// <summary> Gets all. </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public virtual Task<IQueryable<TEntity>> GetAllAsync(CancellationTokenSource cancellationToken)
        {
            try
            {
                return Task.FromResult<IQueryable<TEntity>>(DbSet);
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetAllAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        /// <summary> Gets the by. </summary>
        /// <param name="predicate">         The predicate. </param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="InfraestructureExcepcion"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            try
            {
                return Task.FromResult(DbSet.Where(predicate).AsQueryable());
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByExpressionAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
            throw new NotImplementedException();
        }

        /// <summary> Gets the by key asynchronous. </summary>
        /// <param name="key">               The key. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public virtual Task<TEntity> GetByKeyAsync(TKey key, CancellationTokenSource cancellationToken)
        {
            try
            {
                return DbSet.FindAsync(key);
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        /// <summary> Gets the paged elements. </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">         Index of the page. </param>
        /// <param name="pageCount">         The page count. </param>
        /// <param name="orderByExpression"> The order by expression. </param>
        /// <param name="predicate">         The predicate. </param>
        /// <param name="ascending">         if set to <c> true </c> [ascending]. </param>
        /// <param name="includes">          The includes. </param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public virtual IQueryable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, Expression<Func<TEntity, bool>> predicate, bool ascending, IList<String> includes)
        {
            try
            {
                //filtro
                var query = DbSet.Where(predicate);
                //Ordenamiento
                query = (ascending ? query.OrderBy(orderByExpression) : query.OrderByDescending(orderByExpression));
                //Paginado
                query = query.Skip(pageCount + (pageIndex - 1)).Take(pageCount);
                //Incluir tablas relacionadas
                foreach (var include in includes)
                {
                    query.Include(include);
                }

                return query;
            }
            catch (Exception exc)
            {
                string mensaje = String.Format(MessagesInfraestructure.ErrorPaging, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }
    }
}
