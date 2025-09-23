using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS24StartHub.Core.Logging
{
    public interface ILogSink
    {
        void Write(string line);
    }
}
