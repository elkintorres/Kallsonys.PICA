using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using Kallsonys.PICA.Domain.Entities;
using RepoDb;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ConectionBDB2C"].ConnectionString;
        public Task<int> CountAsync(Expression<Func<B2CProduct, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<B2CProduct> CreateAsync(B2CProduct entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var id = await connection.InsertAsync<int>("B2CProduct", entity);
                    entity.IdProduct = id;
                }

                return entity;
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorCreateAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public void Delete(B2CProduct entity)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<B2CProduct>> GetAllAsync(CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<B2CProduct>> GetByExpressionAsync(Expression<Func<B2CProduct, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {



                    var result = await connection.QueryAsync<B2CProduct>(predicate, hints: "WITH (NOLOCK)");
                    return result.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<IQueryable<B2CProduct>> GetByCodeAsync(string code, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {

                    // Get the parent customer, and the child objects
                    var result = await connection.QueryMultipleAsync<B2CProduct, B2CImage>(
                        product => product.Code == code,
                        image => image.B2CProduct.Code == code);

                    foreach (var product in result.Item1)
                    {
                        product.B2CImage = result.Item2.Where(x => x.IdProduct == product.IdProduct).ToList();
                    }
                    return result.Item1.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<IQueryable<B2CProduct>> GetByExpressionAsync(Expression<Func<B2CProduct, bool>> predicate, int pageCount, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2CProduct>(predicate, top: pageCount, hints: "WITH (NOLOCK)");
                    return result.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<B2CProduct> GetByKeyAsync(int key, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2CProduct>(c => c.IdProduct == key, hints: "WITH (NOLOCK)");
                    return result.FirstOrDefault();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public IQueryable<B2CProduct> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<B2CProduct, S>> orderByExpression, Expression<Func<B2CProduct, bool>> predicate, bool ascending, IList<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<B2CProduct> UpdateAsync(B2CProduct entity, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<B2CProduct>> GetAllAsync(int pageCount, int pageIndex, CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {

                    var commandText = "select top (@pageCount) * from (select ROW_NUMBER() over(order by[IdProduct] asc) as rn, * from [dbo].[B2CProduct] WITH (NOLOCK)) as x where rn between @pageIndex and @pageIndex + @pageCount";
                    var param = new QueryGroup(new[]
                    {
                        new QueryField("pageCount", pageCount),
                        new QueryField("pageIndex", pageIndex)
                    });

                    // Execute the SQL
                    var result = await connection.ExecuteQueryAsync<B2CProduct>(commandText, param);

                    return result.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }
    }
}