using g2v.core.clinetsync.common.Classes.Models;
using g2v.core.clinetsync.common.Interfaces.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.dataaccess.Interfaces
{
    public interface IApplicationDbClient
    {
        Task<IClientResult<Application[]>> GetAllAsync();
        Task<IClientResult<Application>> CreateAsync(Application dto);
        Task<IClientResult<Application>> GetAsync(Guid id);
        Task<IClientResult<Application>> UpdateAsync(Guid id, Application dto);
        Task<IClientResult<Guid>> DeleteAsync(Guid id);
    }
}
