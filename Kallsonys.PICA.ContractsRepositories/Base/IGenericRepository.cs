﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories.Base
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : class, new()
    {
        /// <summary> Creates the asynchronous. </summary>
        /// <param name="entity">            The entity. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<Int32> CreateAsync(TEntity entity, CancellationTokenSource cancellationToken);

        /// <summary> Deletes the specified entity. </summary>
        /// <param name="entity"> The entity. </param>
        void Delete(TEntity entity);

        /// <summary> Updates the asynchronous. </summary>
        /// <param name="entity">            The entity. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<Boolean> UpdateAsync(TEntity entity, CancellationTokenSource cancellationToken);

        /// <summary> Counts the asynchronous. </summary>
        /// <param name="predicate">         The predicate. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationTokenSource cancellationToken);

        /// <summary> Gets all. </summary>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetAllAsync(CancellationTokenSource cancellationToken);

        /// <summary> Gets the by. </summary>
        /// <param name="predicate"> The predicate. </param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate, CancellationTokenSource cancellationToken);

        /// <summary> Gets the by key asynchronous. </summary>
        /// <param name="key">               The key. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<TEntity> GetByKeyAsync(Tkey key, CancellationTokenSource cancellationToken);

        /// <summary> Gets the paged elements. </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex">         Index of the page. </param>
        /// <param name="pageCount">         The page count. </param>
        /// <param name="orderByExpression"> The order by expression. </param>
        /// <param name="predicate">         The predicate. </param>
        /// <param name="ascending">         if set to <c> true </c> [ascending]. </param>
        /// <param name="includes">          The includes. </param>
        /// <returns></returns>
        IQueryable<TEntity> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<TEntity, S>> orderByExpression, Expression<Func<TEntity, bool>> predicate, bool ascending, IList<String> includes);
    }
}