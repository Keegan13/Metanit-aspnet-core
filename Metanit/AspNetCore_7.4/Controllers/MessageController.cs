using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCore_7._4.Models;

namespace AspNetCore_7._4.Controllers
{
    public class MessageController : Controller
    {
        public IEnumerable<Message> messages;

        public MessageController()
        {
            InitializeMessages();
        }

        private void InitializeMessages()
        {
            var initializator = new Message[5];
            for (int i = 0; i < initializator.Count(); i++)
            {
                User u = new User { Id = i + 10, UserName = "User" + i };
                Message m = new Message { User = u, Created = DateTime.Now, id = i + 6, Content = "Content #" + (i + 6).ToString(), UserId = u.Id };
                u.Messages = new[] { m };
                initializator[i] = m;
            }
            this.messages = initializator;
        }

        public IActionResult Chat()
        {
            ////var collection = from item in this.messages
            ////                 select item.GetViewModel();

            ////object obj = this.messages.FirstOrDefault();


            ////var view = (MessageViewModel)obj;


            //var view = (MessageViewModel)this.messages.FirstOrDefault();

            //var collection = new List<MessageViewModel>();

            //foreach (var item in this.messages)
            //{
            //    collection.Add((MessageViewModel)item);
            //}

            return View(from item in this.messages select item.GetViewModel());
        }

    }
}