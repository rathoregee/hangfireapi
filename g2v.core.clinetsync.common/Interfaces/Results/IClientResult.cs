using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.common.Interfaces.Results
{
    public interface IClientResult : IResult
    {
    }

    public interface IClientResult<out T> : IResult<T>, IClientResult
    {
    }
}
