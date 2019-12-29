using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDbUpdate.Client.Interop
{
    public class InteropMethods
    {
        public static Action OnDataChanged;

        [JSInvokable]
        public static void DataChanged()
        {
            Console.WriteLine("Successfully received notification that data has changed from Server SignalR Hub");
            OnDataChanged.Invoke();
        }

    }
}
