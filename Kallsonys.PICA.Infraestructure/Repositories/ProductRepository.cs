using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using Kallsonys.PICA.Domain.Entities;
using RepoDb;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        protected string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ConectionBDB2C"].ConnectionString;
        public Task<int> CountAsync(Expression<Func<B2CProduct, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Int32> CreateAsync(B2CProduct entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var id = await connection.InsertAsync<int>("B2CProduct", entity);
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
                    var products = await connection.QueryAsync<B2CProduct>(predicate, hints: "WITH (NOLOCK)");
                    var images = await connection.QueryAsync<B2CImage>(x => products.Select(w => w.IdProduct).Contains(x.IdProduct) && x.IdOffer == null, hints: "WITH (NOLOCK)");

                    foreach (var product in products)
                    {
                        product.B2CImage = images.Where(x => x.IdProduct == product.IdProduct).ToList();
                    }

                    return products.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<IQueryable<B2CProduct>> GetAllByIdsAsync(IList<int> ids, CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    // Get the parent customer, and the child objects
                    var result = await connection.QueryMultipleAsync<B2CProduct, B2CImage>(
                        product => ids.Contains(product.IdProduct),
                        image => ids.Contains(image.B2CProduct.IdProduct) && image.IdOffer == null);

                    foreach (var product in result.Item1)
                    {
                        product.B2CImage = result.Item2.Where(x => x.IdProduct == product.IdProduct).ToList();
                    }
                    return result.Item1.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<IQueryable<B2CProduct>> GetByCodeAsync(string code, int pageSize, int pageIndex, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {

                    int startPage = (pageSize * (pageIndex - 1)), endPage = startPage + pageSize;
                    startPage += 1;

                    string filterSql = $"{code}";

                    var commandText = $"select * from (select ROW_NUMBER() over(order by[IdProduct] asc) as rn, * from [dbo].[B2CProduct] WITH (NOLOCK) where code = @filterSql and IsActive = 1) as x where rn between @startPage and @endPage";
                    var param = new QueryGroup(new[]
                    {
                        new QueryField("filterSql", filterSql),
                        new QueryField("startPage", startPage),
                        new QueryField("endPage", endPage)
                    });

                    // Execute the SQL
                    var products = await connection.ExecuteQueryAsync<B2CProduct>(commandText, param);

                    if (products.Any())
                    {
                        commandText = "SELECT * FROM [dbo].[B2CImage]  WITH (NOLOCK) WHERE IdOffer is null and IdProduct IN (@Keys);";
                        param = new QueryGroup(new QueryField("Keys", products.Select(w => w.IdProduct).ToArray()));

                        var images = await connection.ExecuteQueryAsync<B2CImage>(commandText, param);

                        foreach (var product in products)
                        {
                            product.B2CImage = images.Where(x => x.IdProduct == product.IdProduct).ToList();
                        }
                    }
                    return products.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                cancellationToken.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<IQueryable<B2CProduct>> GetByCriteria(string criteria, int pageSize, int pageIndex, CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    int startPage = (pageSize * (pageIndex - 1)), endPage = startPage + pageSize;
                    startPage += 1;

                    string filterSql = "";
                    var criteriaSql = criteria.Replace("@", "");

                    if (criteria.StartsWith("@"))
                        filterSql = $"%{criteriaSql}";
                    else if (criteria.EndsWith("@"))
                        filterSql = $"{criteriaSql}%";
                    else
                        filterSql = $"{criteriaSql}";

                    var commandText = $"select * from (select ROW_NUMBER() over(order by [IdProduct] asc) as rn, * from [dbo].[B2CProduct] WITH (NOLOCK) where  name like @filterSql or description like @filterSql and IsActive = 1) as x where rn between @startPage and @endPage";
                    var param = new QueryGroup(new[]
                    {
                        new QueryField("filterSql", filterSql),
                        new QueryField("startPage", startPage),
                        new QueryField("endPage", endPage)
                    });

                    // Execute the SQL
                    var products = await connection.ExecuteQueryAsync<B2CProduct>(commandText, param);

                    if (products.Any())
                    {
                        commandText = "SELECT * FROM [dbo].[B2CImage]  WITH (NOLOCK) WHERE IdOffer is null and IdProduct IN (@Keys);";
                        param = new QueryGroup(new QueryField("Keys", products.Select(w => w.IdProduct).ToArray()));

                        var images = await connection.ExecuteQueryAsync<B2CImage>(commandText, param);

                        foreach (var product in products)
                        {
                            product.B2CImage = images.Where(x => x.IdProduct == product.IdProduct).ToList();
                        }
                    }
                    return products.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }
        public async Task<IQueryable<B2CProduct>> GetByExpressionAsync(Expression<Func<B2CProduct, bool>> predicate, int pageSize, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2CProduct>(predicate, top: pageSize, hints: "WITH (NOLOCK)");
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
                    var result = await connection.QueryMultipleAsync<B2CProduct, B2CImage>(
                      product => product.IdProduct == key,
                      image => image.IdProduct == key && image.IdOffer == null,
                      top1: 100,
                      hints1: "WITH (NOLOCK)",
                      hints2: "WITH (NOLOCK)");

                    foreach (var product in result.Item1)
                    {
                        product.B2CImage = result.Item2.Where(x => x.IdProduct == product.IdProduct).ToList();
                    }

                    return result.Item1.FirstOrDefault();
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

        public async Task<IQueryable<B2CProduct>> GetAllAsync(int pageSize, int pageIndex, CancellationTokenSource token)
        {
            try
            {
                int startPage = (pageSize * (pageIndex - 1)), endPage = startPage + pageSize;

                startPage += 1;
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {

                    var commandText = "select * from (select ROW_NUMBER() over(order by[IdProduct] asc) as rn, * FROM [dbo].[B2CProduct] WITH (NOLOCK) WHERE IsActive = 1) as x where rn between @startPage and @endPage";
                    var param = new QueryGroup(new[]
                    {
                        new QueryField("startPage", startPage),
                        new QueryField("endPage", endPage)
                    });

                    // Execute the SQL
                    var products = await connection.ExecuteQueryAsync<B2CProduct>(commandText, param);
                    if (products.Any())
                    {
                        commandText = "SELECT * FROM [dbo].[B2CImage]  WITH (NOLOCK) WHERE IdOffer is null and IdProduct IN (@Keys);";
                        param = new QueryGroup(new QueryField("Keys", products.Select(w => w.IdProduct).ToArray()));

                        var images = await connection.ExecuteQueryAsync<B2CImage>(commandText, param);

                        foreach (var product in products)
                        {
                            product.B2CImage = images.Where(x => x.IdProduct == product.IdProduct).ToList();
                        }
                    }
                    return products.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public async Task<int> GetCountAll(CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.ExecuteScalarAsync<Int32>("SELECT COUNT (1) AS [CountValue] FROM [B2CProduct] WITH (NOLOCK) WHERE IsActive = 1 ;");
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }

        }

        public async Task<bool> DisableById(int id, CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2CProduct>(x => x.IdProduct == id, hints: "WITH (NOLOCK)");
                    if (result.Any())
                    {
                        var product = result.First();
                        product.IsActive = false;
                        var affectedRows = connection.Update("B2CProduct", product, new QueryField("IdProduct", id));
                        return affectedRows > 0;
                    }
                    else
                        return false;
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