using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDbUpdate.Server.Hubs
{
    public class NotificationHub : Hub
    {

        public override async Task OnConnectedAsync()
        {
            Debug.Write("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Server: DataTime: {0} - Signalr client has connected.", DateTime.Now.ToString());
        }

    }
}
