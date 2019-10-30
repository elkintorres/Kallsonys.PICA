using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories.Base
{
    public interface IGenericRepository<TEntity, Tkey> : IReadRepository<TEntity, Tkey> where TEntity : class, new()
    {
        /// <summary> Creates the asynchronous. </summary>
        /// <param name="entity">            The entity. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity, CancellationTokenSource cancellationToken);

        /// <summary> Deletes the specified entity. </summary>
        /// <param name="entity"> The entity. </param>
        void Delete(TEntity entity);

        /// <summary> Updates the asynchronous. </summary>
        /// <param name="entity">            The entity. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationTokenSource cancellationToken);
    }
}