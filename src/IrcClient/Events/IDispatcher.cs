using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public interface IDispatcher<Source, Dispatchee>
    {

        void Invoke(Source source, Dispatchee dispatchee);

    }
}
