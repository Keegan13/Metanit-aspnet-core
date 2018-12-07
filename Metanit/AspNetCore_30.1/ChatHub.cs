using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AspNetCore_30._1
{
    public class ChatHub:Hub
{
        public async Task Send(string message)
        {
            await this.Clients.All.SendAsync("Send",message);
        }
}
}
