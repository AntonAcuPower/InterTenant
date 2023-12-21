using System;

namespace Interco.Middle.Transfers
{
    public interface IWorkerContext
    {
        bool ExceptionThrown { get; }
        Exception Exception { get; set; }
    }
}
