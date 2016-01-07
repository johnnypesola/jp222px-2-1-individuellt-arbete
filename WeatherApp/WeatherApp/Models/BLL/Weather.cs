using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WeatherApp.Models
{
    [MetadataType(typeof(Weather_Metadata))]
    public partial class Weather
    {
        internal sealed class Weather_Metadata
        {
            public int WeatherId;

            public int PlaceId;

            [Display(Name = "Datum")]
            [Required(ErrorMessage = "Var god ange ett Datum.")]
            public System.DateTime DateTime { get; set; }

            [Display(Name = "Temperatur")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Temperatur.")]
            [Range(-999, 999, ErrorMessage = "Värdet för Temperatur får max vara 999 och minst -999.")]
            public decimal Temperature { get; set; }

            [Display(Name = "Vindriktning")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Vindriktning.")]
            [Range(0, 360, ErrorMessage = "Värdet för Vindriktning får max vara 360 och minst 0.")]
            public int WindDirection { get; set; }

            [Display(Name = "Vindhastighet")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Vindhastighet.")]
            [Range(0, 999, ErrorMessage = "Värdet för Vindhastighet får max vara 999 och minst 0.")]
            public decimal WindSpeed { get; set; }

            [Display(Name = "Luftfuktighet")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Luftfuktighet.")]
            [Range(0, 100, ErrorMessage = "Värdet för Luftfuktighet får max vara 100 och minst 0.")]
            public byte Humidity { get; set; }

            [Display(Name = "Nederbördsform")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Nederbördsform.")]
            [Range(0, 6, ErrorMessage = "Värdet för Nederbördsform får max vara 6 och minst 0.")]
            public byte Precipitation { get; set; }

            [Display(Name = "Molnmängd")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Molnmängd.")]
            [Range(0, 8, ErrorMessage = "Värdet för Molnmängd får max vara 8 och minst 0.")]
            public byte TotalCloudCover { get; set; }

            [Display(Name = "Sannolikhet för åska")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Sannolikhet för åska.")]
            [Range(0, 100, ErrorMessage = "Värdet för Molnmängd får max vara 100 och minst 0.")]
            public byte ThunderStormProbability { get; set; }

            [Display(Name = "Nederbörd")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Required(ErrorMessage = "Var god ange Nederbörd.")]
            [Range(0, 255, ErrorMessage = "Värdet för Nederbörd får max vara 255 och minst 0.")]
            public decimal PrecipitationIntensity { get; set; }
        }

        // To offer data annotation validation in service layer
        public bool IsValid(ref Dictionary<string, string> errors)
        {
            return this.IsDataAnnotationValid<Weather, Weather_Metadata>(ref errors);
        }
    }
}
