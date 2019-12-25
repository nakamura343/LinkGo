using System;
using System.Collections.Generic;

namespace LinkGo.Common.Event
{
    /// <summary>
    /// 一个泛型实现的事件系统
    /// </summary>
    public static class EventManager
    {
        static Dictionary<int, Delegate> s_EventTableByIntKey = new Dictionary<int, Delegate>();

        #region AddListener
        public static void AddListener(int eventId, EventHandler handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1>(int eventId, EventHandler<T1> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2>(int eventId, EventHandler<T1, T2> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3>(int eventId, EventHandler<T1, T2, T3> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4>(int eventId, EventHandler<T1, T2, T3, T4> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4, T5>(int eventId, EventHandler<T1, T2, T3, T4, T5> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4, T5, T6>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4, T5, T6, T7>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7, T8> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7, T8>)s_EventTableByIntKey[eventId] + handler;
        }

        public static void AddListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler)
        {
            // Only take action if this event type exists.
            if (!s_EventTableByIntKey.ContainsKey(eventId))
            {
                s_EventTableByIntKey.Add(eventId, null);
            }
            // Add the handler to the event.
            s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>)s_EventTableByIntKey[eventId] + handler;
        }
        #endregion

        #region RemoveListener
        public static void RemoveListener(int eventId, EventHandler handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1>(int eventId, EventHandler<T1> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2>(int eventId, EventHandler<T1, T2> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3>(int eventId, EventHandler<T1, T2, T3> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4>(int eventId, EventHandler<T1, T2, T3, T4> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4, T5>(int eventId, EventHandler<T1, T2, T3, T4, T5> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4, T5, T6>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4, T5, T6, T7>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7, T8> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7, T8>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void RemoveListener<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9> handler)
        {
            if (s_EventTableByIntKey.ContainsKey(eventId))
            {
                // Remove the event handler from this event.
                s_EventTableByIntKey[eventId] = (EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>)s_EventTableByIntKey[eventId] - handler;

                // If there's nothing left then remove the event type from the event table.
                if (s_EventTableByIntKey[eventId] == null)
                {
                    s_EventTableByIntKey.Remove(eventId);
                }
            }
            else
            {
                throw new Exception(string.Format("Don't Remove Listener,EventId:{0} don't exist!!!", eventId));
            }
        }
        #endregion

        #region Broadcast
        public static void Dispatcher(int eventId)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler)?.Invoke();
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1>(int eventId, T1 arg1)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1>)?.Invoke(arg1);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2>(int eventId, T1 arg1, T2 arg2)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2>)?.Invoke(arg1, arg2);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3>(int eventId, T1 arg1, T2 arg2, T3 arg3)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4>)?.Invoke(arg1, arg2, arg3, arg4);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4, T5>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4, T5>)?.Invoke(arg1, arg2, arg3, arg4, arg5);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4, T5, T6>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4, T5, T6>)?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4, T5, T6, T7>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4, T5, T6, T7>)?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4, T5, T6, T7, T8>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4, T5, T6, T7, T8>)?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }

        public static void Dispatcher<T1, T2, T3, T4, T5, T6, T7, T8, T9>(int eventId, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7, T8 arg8, T9 arg9)
        {
            Delegate d;
            if (s_EventTableByIntKey.TryGetValue(eventId, out d))
            {
                (d as EventHandler<T1, T2, T3, T4, T5, T6, T7, T8, T9>)?.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
            else
            {
                throw new Exception(string.Format("Don't Dispatcher Event,EventId:{0} don't exist!!!", eventId));
            }
        }
        #endregion
    }
}
