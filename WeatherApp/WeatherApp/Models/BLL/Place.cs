using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{

    [MetadataType(typeof(Place_Metadata))]
    public partial class Place
    {
        internal sealed class Contact_Metadata
        {
            public int PlaceId;

            [Display(Name = "Namn")]
            [Required(ErrorMessage = "Var god ange ett namn.")]
            [MaxLength(50, ErrorMessage = "Namnet får max bestå av 50 tecken")]
            [RegularExpression(ValidationExtensions.TEXT_FIELD_REGEXP, ErrorMessage = "Namnet innehåller otillåtna tecken.")]
            public string Name;

            [Display(Name = "Efternamn")]
            [Required(ErrorMessage = "Var god ange ett efternamn.")]
            [MaxLength(50, ErrorMessage = "Efternamnet får max bestå av 50 tecken")]
            [RegularExpression(ValidationExtensions.TEXT_FIELD_REGEXP, ErrorMessage = "Efternamnet innehåller otillåtna tecken.")]
            public string LastName;

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange ett GPS longitud värde.")]
            [Range(-180, 180, ErrorMessage = "Värdet för longitud får max vara 180 och minst -180.")]
            public decimal Longitude { get; set; }

            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange ett GPS latitud värde.")]
            [Range(-86, 86, ErrorMessage = "Värdet för latitud får max vara 86 och minst -86.")]
            public decimal Latitude { get; set; }

            [Display(Name = "E-post")]
            [Required(ErrorMessage = "Var god ange en e-postadress.")]
            [EmailAddress(ErrorMessage = "Var god ange en giltig e-postadress")]
            [MaxLength(50, ErrorMessage = "Adressen får max bestå av 50 tecken")]
            public string EmailAddress;
        }
    }
}