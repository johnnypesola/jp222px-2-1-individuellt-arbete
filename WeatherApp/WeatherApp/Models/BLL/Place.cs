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
        internal sealed class Place_Metadata
        {
            public int PlaceId;

            [Display(Name = "Namn")]
            [Required(ErrorMessage = "Var god ange ett namn.")]
            [MaxLength(50, ErrorMessage = "Namnet får max bestå av 50 tecken")]
            [RegularExpression(ValidationExtensions.TEXT_FIELD_REGEXP, ErrorMessage = "Namnet innehåller otillåtna tecken.")]
            public string Name;

            [Display(Name = "Latitud")]
            [DisplayFormat(ConvertEmptyStringToNull = false, DataFormatString = "{0:n15}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Var god ange ett GPS longitud värde.")]
            [Range(-180, 180, ErrorMessage = "Värdet för longitud får max vara 180 och minst -180.")]
            public decimal Longitude { get; set; }

            [Display(Name = "Longitud")]
            [DisplayFormat(ConvertEmptyStringToNull = false, DataFormatString = "{0:n15}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Var god ange ett GPS latitud värde.")]
            [Range(-86, 86, ErrorMessage = "Värdet för latitud får max vara 86 och minst -86.")]
            public decimal Latitude { get; set; }
        }
    }
}