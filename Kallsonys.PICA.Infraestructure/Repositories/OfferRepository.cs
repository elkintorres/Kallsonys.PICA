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
    public class OfferRepository : IOfferRepository
    {
        protected string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings["ConectionBDB2C"].ConnectionString;
        public Task<int> CountAsync(Expression<Func<B2COffer, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Int32> CreateAsync(B2COffer entity, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var id = await connection.InsertAsync<int>("B2COffer", entity);
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

        public void Delete(B2COffer entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<B2COffer>> GetActiveOffers(int pageSize, int pageIndex, CancellationTokenSource token)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {

                    int startPage = (pageSize * (pageIndex - 1)), endPage = startPage + pageSize;
                    startPage += 1;

                    var commandText = $"select * from (select ROW_NUMBER() over(order by [IdOffer] asc) as rn, * from [dbo].[B2COffer] WITH (NOLOCK) where GETDATE() between BeginDate and EndDate and IsActive = 0) as x where rn between @startPage and @endPage";
                    var param = new QueryGroup(new[]
                    {
                        new QueryField("startPage", startPage),
                        new QueryField("endPage", endPage)
                    });

                    // Execute the SQL
                    var offers = await connection.ExecuteQueryAsync<B2COffer>(commandText, param);

                    if (offers.Any())
                    {
                        commandText = "SELECT * FROM [dbo].[B2CImage]  WITH (NOLOCK) WHERE IdOffer IN (@Keys);";
                        param = new QueryGroup(new QueryField("Keys", offers.Select(w => w.IdOffer).ToArray()));

                        var images = await connection.ExecuteQueryAsync<B2CImage>(commandText, param);

                        foreach (var offer in offers)
                        {
                            offer.B2CImage = images.Where(x => x.IdOffer == offer.IdOffer).ToList();
                        }
                    }
                    return offers.AsQueryable();
                }
            }
            catch (Exception exc)
            {
                token.Cancel(true);
                string mensaje = String.Format(MessagesInfraestructure.ErrorGetByKeyAsync, "en Repositorio base");
                throw new InfraestructureExcepcion(mensaje, exc);
            }
        }

        public Task<IQueryable<B2COffer>> GetAllAsync(CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<B2COffer>> GetByExpressionAsync(Expression<Func<B2COffer, bool>> predicate, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2COffer>(predicate, hints: "WITH (NOLOCK)");
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

        public async Task<B2COffer> GetByKeyAsync(int key, CancellationTokenSource cancellationToken)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString).EnsureOpen())
                {
                    var result = await connection.QueryAsync<B2COffer>(c => c.IdOffer == key, hints: "WITH (NOLOCK)");
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

        public IQueryable<B2COffer> GetPagedElements<S>(int pageIndex, int pageCount, Expression<Func<B2COffer, S>> orderByExpression, Expression<Func<B2COffer, bool>> predicate, bool ascending, IList<string> includes)
        {
            throw new NotImplementedException();
        }

        public Task<B2COffer> UpdateAsync(B2COffer entity, CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}