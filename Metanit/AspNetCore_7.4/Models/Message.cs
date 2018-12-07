using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore_7._4.Models
{
    public class Message : MessageBase
{

    #region Properties

    [NotMapped]
    public override string Author { get => base.Author; set => base.Author = value; }
    public int id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
        #endregion

        public static explicit operator MessageViewModel(Message message)
        {
            return new MessageViewModel
            {
                Author = message.User?.UserName,
                Content = message.Content,
                Created = message.Created
            };
        }

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

public class MessageViewModel:MessageBase
{

}

public abstract class MessageBase
{
    public virtual DateTime Created { get; set; }
    public virtual string Content { get; set; }
    public virtual string Author { get; set; }   
}

public class User {

    public int Id { get; set; }
    public string UserName { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}
}
