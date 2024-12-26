using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Core.SignalR
{
    public class CommentHub : Hub
    {
        protected IHubContext<CommentHub> _context;

        public CommentHub([FromServices] IHubContext<CommentHub> context)
        {
            _context = context;
        }
        public async Task SendComment(string user, string message)
        {
            await _context.Clients.All.SendAsync("MessageReceived", user, message);
        }

        [HubMethodName("GotComment")]
        public async Task GotComment(string message)
        {
            Console.WriteLine("got message Ehere:" + message);
        }
    }
}
