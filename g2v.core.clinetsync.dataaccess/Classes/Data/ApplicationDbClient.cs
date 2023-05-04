using g2v.core.clinetsync.common.Classes.Models;
using g2v.core.clinetsync.common.Classes.Results;
using g2v.core.clinetsync.common.Interfaces.Results;
using g2v.core.clinetsync.dataaccess.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            try
            {
                _dataContext.Add(dto);
                await _dataContext.SaveChangesAsync();
                return ClientResult.Created(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Application>();
            }
        }

        public async Task<IClientResult<Guid>> DeleteAsync(Guid id)
        {
            try
            {
                var application = await _dataContext.Applications.FirstOrDefaultAsync(x => x.Id == id);
                if (application == null)
                {
                    return ClientResult.NotFound<Guid>();
                }

                _dataContext.Remove(application);
                await _dataContext.SaveChangesAsync();
                return ClientResult.Deleted<Guid>(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Guid>();
            }
        }

        public async Task<IClientResult<Application[]>> GetAllAsync()
        {
            try
            {
                var applications = await _dataContext.Applications.ToArrayAsync();
               
                return ClientResult.Success(applications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Application[]>();
            }
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

                return ClientResult.Success(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Application>();
            }
        }    

        public async Task<IClientResult<Application>> UpdateAsync(Guid id, Application dto)
        {
            try
            {
                var application = await _dataContext.Applications.FirstOrDefaultAsync(x => x.Id == id);

                if (application == null)
                {
                    return ClientResult.NotFound<Application>();
                }              

                _dataContext.Attach(application);
                await _dataContext.SaveChangesAsync();
                return ClientResult.Updated(application);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Database error");
                return ClientResult.ServiceUnavailable<Application>();
            }
        }
    }
}
