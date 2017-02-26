using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Irsee.IrcClient.Events
{
    public class GenericDispatcher<Key, Dispatchee, Source>
    {
        private IDictionary<Key, List<Dispatcher>> handlers { get; set; }
            = new Dictionary<Key, List<Dispatcher>>();

        public delegate void Dispatcher(Source source, Dispatchee dispatchee);

        public void AddHandler(Key Key, Dispatcher handler)
        {
            List<Dispatcher> handlerList;
            if (!TryGetValue(Key, out handlerList))
            {
                handlerList = new List<Dispatcher>(1);
            }
            handlerList.Add(handler);
            handlers[Key] = handlerList;
        }

        public void Dispatch(Key key, Source source, Dispatchee dispatchee)
        {
            List<Dispatcher> handlers;
            // Console.WriteLine($"DEBUG: Dispatching {message}...");
            if (TryGetValue(key, out handlers))
            {
                foreach (var handler in handlers)
                {
                    Console.WriteLine($"DEBUG: Dispatching {dispatchee} to {handler}");
                    handler.Invoke(source, dispatchee);
                }
            }
        }

        public List<Dispatcher> this[Key index]
        {
            get { return handlers[index]; }
            set { handlers[index] = value; }
        }

        public bool TryGetValue(Key Key, out List<Dispatcher> handlerList)
        {
            return handlers.TryGetValue(Key, out handlerList);
        }

        public bool RemoveHandler(Key Key, Dispatcher handler)
        {
            List<Dispatcher> handlerList;
            if (TryGetValue(Key, out handlerList))
            {
                handlerList.Remove(handler);
                handlers[Key] = handlerList;
                return true;
            }
            return false;
        }

        public bool RemoveAllHandlers(Key Key)
        {
            return handlers.Remove(Key);
        }
    }
}
