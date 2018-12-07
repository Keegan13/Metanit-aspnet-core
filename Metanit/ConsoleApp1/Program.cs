using System;
using AspNetCore_7._4.Models;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {


        private static IEnumerable<Message> InitializeMessages()
        {
            var messages = new Message[5];
            for (int i = 0; i < messages.Count(); i++)
            {
                User u = new User { Id = i + 10, UserName = "User" + i };
                Message m = new Message { User = u, Created = DateTime.Now, id = i + 6, Content = "Content #" + (i + 6).ToString(), UserId = u.Id };
                u.Messages = new[] { m };
                messages[i] = m;
            }
            return messages;
        }
        static void Main(string[] args)
        {

            
            Message message = InitializeMessages().FirstOrDefault();

            MessageViewModel ViewModel = (MessageViewModel)message;


            Console.WriteLine(ViewModel.Author);
            Console.ReadKey();
        }
    }

    public class ClassParent
    {
        public int id { get; set; }
    }
    public class Child1 : ClassParent
    {
        public string Name { get; set; }
    }
    public class Child2 : ClassParent

    {
        public string Adress { get; set; }
    }


}

namespace AspNetCore_7._4.Models
{
    public class Message : MessageBase
    {

        #region Properties


        public override string Author { get => base.Author; set => base.Author = value; }
        public int id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        #endregion

        public static implicit operator MessageViewModel(Message message)
        {
            return new MessageViewModel
            {
                Author = message.User?.UserName,
                Content = message.Content,
                Created = message.Created
            };
        }
        //public static explicit operator MessageViewModel()
        //{
        //    retrun message:
        //}

        public MessageViewModel GetViewModel()
        {
            return GetViewModel(this);
        }
        public static MessageViewModel GetViewModel(Message message)
        {
            if (message.User == null)
                throw new NullReferenceException(nameof(message.User));

            return new MessageViewModel { Content = message.Content, Created = message.Created, Author = message.User?.UserName };
        }
    }

    public class MessageViewModel : MessageBase
    {

    }

    public abstract class MessageBase
    {
        public virtual DateTime Created { get; set; }
        public virtual string Content { get; set; }
        public virtual string Author { get; set; }
    }

    public class User
    {

        public int Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<Message> Messages { get; set; }
    }
}