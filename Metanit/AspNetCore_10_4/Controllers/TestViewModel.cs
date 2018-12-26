using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore_10_4.Controllers
{
    public class TestViewModel
    {
        //some text field
        [Required]
        public string Comment { get; set; }
    }
}