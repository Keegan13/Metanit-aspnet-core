using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace AspNetCore_10_4.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(50, 500)]
        [Required]
        [RegularExpression("^*$",ErrorMessage ="############")]
        public int Price { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    //kinda viewmodel
    public class Profile
    {
        //do not bind property
        public int Id { get; set; }
        //bind requeired,required
        public string Password { get; set; }

        //

        //required

        public int CountryId { get; set; }
        //custom format
        public DateTime Birthday { get; set; }

        public IEnumerable<string> Tags { get; set; }
        // enumerable

    }

    public enum Countries
    {
        [Display(Name ="United States")]
        UnitedStates,
        [Display(Name = "United Kingdom")]
        UnitedKingdom,
        [Display(Name = "England")]
        England
    }

    public class Country : ICountry
    {
        public static readonly string UnitedStates = "United States";
        public static readonly string UnitedKingdom = "United Kingdom";
        public static readonly string England = "England";

        public string Name { get; set; }
        public Countries EnumCountry { get; set; }

        public static explicit operator Country(Countries enumCountry)
        {
            var field = typeof(Country).GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(x => x.Name == enumCountry.ToString());
            if (field == null)
                return default(Country);
            else
                return new Country { EnumCountry=enumCountry,Name = field.GetValue(null) as string };
        }



    }
    public interface ICountry
    {
        string Name { get; set; }
        Countries EnumCountry { get; set;}
    }
}
