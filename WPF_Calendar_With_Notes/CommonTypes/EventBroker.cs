using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes.CommonTypes
{
    public class EventBroker : IEventBroker
    {
        private readonly Dictionary<EventType, List<IListener>> Listeners;
        public EventBroker()
        {
            Listeners = new Dictionary<EventType, List<IListener>>();
        }

        public void RegisterFor(EventType type, IListener listener)
        {
            if (Listeners.ContainsKey(type) == false)
            {
                Listeners.Add(type, new List<IListener>());
            }

            var listenersList = Listeners[type];
            if (listenersList.Contains(listener) == false)
                listenersList.Add(listener);
        }


        public void FireEvent(EventType type, object data)
        {
            if (Listeners.ContainsKey(type) == false)
                return;

            var listenersList = Listeners[type];
            listenersList.ForEach(l => l.NotifyMe(type, data));
        }

        public void UnregisterFrom(EventType type, IListener listener)
        {
            if (Listeners.ContainsKey(type) == false)
            {
                return;
            }

            var listenersList = Listeners[type];
            if (listenersList.Contains(listener))
                listenersList.Remove(listener);
        }

        public void UnregisterFromAll(IListener listener)
        {
            foreach (var eventType in Listeners.Keys)
            {
                if (Listeners[eventType].Contains(listener))
                    Listeners[eventType].Remove(listener);
            }
        }
    }
}
