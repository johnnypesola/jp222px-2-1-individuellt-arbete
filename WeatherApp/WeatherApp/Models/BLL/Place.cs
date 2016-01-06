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
        // Public properties
        public string UrlFriendlyLongitude
        {
            get
            {
                return Longitude.ToString().Replace(",", ".").Substring(0, 5);
            }
        }

        public string UrlFriendlyLatitude
        {
            get
            {
                return Latitude.ToString().Replace(",", ".").Substring(0, 5);
            }
        }

        internal sealed class Place_Metadata
        {
            public int PlaceId { get; set; }

            [Display(Name = "Namn")]
            [Required(ErrorMessage = "Var god ange ett namn.")]
            [MaxLength(50, ErrorMessage = "Namnet får max bestå av 50 tecken")]
            [RegularExpression(ValidationExtensions.TEXT_FIELD_REGEXP, ErrorMessage = "Namnet innehåller otillåtna tecken.")]
            public string Name { get; set; }

            [Display(Name = "Longitud")]
            [DisplayFormat(ConvertEmptyStringToNull = false, DataFormatString = "{0:n15}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Var god ange ett GPS longitud värde.")]
            [Range(2.25, 38.00, ErrorMessage = "Värdet för longitud får minst vara 2.25 och max 38.00.")]
            public decimal Longitude { get; set; }

            [Display(Name = "Latitud")]
            [DisplayFormat(ConvertEmptyStringToNull = false, DataFormatString = "{0:n15}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Var god ange ett GPS latitud värde.")]
            [Range(52.50, 70.75, ErrorMessage = "Värdet för latitud får minst vara 52.50 och max 70.75.")]
            public decimal Latitude { get; set; }
        }

        // To offer data annotation validation in service layer
        public bool IsValid(ref Dictionary<string, string> errors)
        {
            return this.IsDataAnnotationValid<Place, Place_Metadata>(ref errors);
        }
    }
}