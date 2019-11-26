using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RepoDb;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        protected string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ConectionBDB2C"].ConnectionString;

        public Task<int> CountAsync(Expression<Func<B2CImage, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(B2CImage entity, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(IList<B2CImage> entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var id = await connection.InsertAllAsync<B2CImage>(entity);
                    return id;
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorCreateAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public void Delete(B2CImage entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<B2CImage>> GetAllAsync(CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<B2CImage>> GetByExpressionAsync(Expression<Func<B2CImage, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<B2CImage> GetByKeyAsync(int key, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public IQueryable<B2CImage> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<B2CImage, S>> orderByExpression, Expression<Func<B2CImage, bool>> predicate, bool ascending, IList<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<Boolean> UpdateAsync(B2CImage entity, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
