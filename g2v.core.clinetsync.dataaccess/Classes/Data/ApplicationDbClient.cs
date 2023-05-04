using g2v.core.clinetsync.common.Classes.Models;
using g2v.core.clinetsync.common.Classes.Results;
using g2v.core.clinetsync.common.Interfaces.Results;
using g2v.core.clinetsync.dataaccess.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.dataaccess.Classes.Data
{
    public class ApplicationDbClient : IApplicationDbClient
    {
        private readonly IDataContext _dataContext;
        private readonly ILogger _logger;

        public ApplicationDbClient(IDataContext dataContext, ILogger logger)
        {
            _logger = logger;
            _dataContext = dataContext;
        }

        public async Task<IClientResult<Application>> CreateAsync(Application dto)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        public async Task<IClientResult<Guid>> DeleteAsync(Guid id)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        public async Task<IClientResult<Application[]>> GetAllAsync()
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }

        public async Task<IClientResult<Application>> GetAsync(Guid id)
        {
            try
            {
                var application = await Task.FromResult(_dataContext.Applications.FirstOrDefault(x => x.Id == id));
                if (application == null)
                {
                    return ClientResult.NotFound<Application>();
                }

                return ClientResult.Success(new Application());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Application>();
            }
        }    

        public async Task<IClientResult<Application>> UpdateAsync(Guid id, Application dto)
        {
            await Task.Delay(0);
            throw new NotImplementedException();
        }
    }
}
