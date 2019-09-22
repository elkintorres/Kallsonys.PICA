using Kallsonys.PICA.ContractsRepositories.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;

namespace Kallsonys.PICA.Infraestructure.Base
{
    public abstract class GenericRepository<TEntity, TKey> : ReadRepositoryGeneric<TEntity, TKey>, IGenericRepository<TEntity, TKey> where TEntity : class, new() where TKey : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="context"> The context. </param>
        public GenericRepository(DbContext context) : base(context)
        {
            this.context = context;
            this.context.Configuration.LazyLoadingEnabled = false;
            this.context.Configuration.ProxyCreationEnabled = false;
        }

        /// <summary> Creates the asynchronous. </summary>
        /// <param name="entity"> The entity. </param>
        /// <returns></returns>
        public virtual Task<TEntity> CreateAsync(TEntity entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                DbSet.Add(entity);
                context.SaveChangesAsync();
                return Task.FromResult(entity);
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorCreateAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        /// <summary> Deletes the specified entity. </summary>
        /// <param name="entity"> The entity. </param>
        /// <exception cref="System.Exception"></exception>
        public virtual void Delete(TEntity entity)
        {
            try
            {
                if (context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
            catch (Exception exc)
            {
                string mensaje = String.Format(MessagesInfraestructure.ErrorDelete, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        /// <summary> Updates the asynchronous. </summary>
        /// <param name="entity">            The entity. </param>
        /// <param name="cancellationToken"> The cancellation token. </param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                DbSet.Attach(entity);
                context.Entry(entity).CurrentValues.SetValues(entity);

                context.Entry(entity).State = EntityState.Modified;
                context.SaveChangesAsync(cancellationToken.Token);
                return Task.FromResult(entity);
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorUpdateAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }
    }
}
