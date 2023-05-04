using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace g2v.core.clinetsync.common.Interfaces.Results
{
    public interface IResult
    {
        string Status { get; }
        object PayloadAsObject { get; }
        string[] Errors { get; }
    }
    public interface IResult<out T> : IResult
    {
        T Payload { get; }
    }
}
