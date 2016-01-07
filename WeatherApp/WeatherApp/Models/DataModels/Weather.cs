//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WeatherApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Weather
    {
        public int WeatherId { get; set; }
        public int PlaceId { get; set; }
        public System.DateTime DateTime { get; set; }
        public decimal Temperature { get; set; }
        public int WindDirection { get; set; }
        public decimal WindSpeed { get; set; }
        public byte Humidity { get; set; }
        public byte Precipitation { get; set; }
        public byte TotalCloudCover { get; set; }
        public byte ThunderStormProbability { get; set; }
        public decimal PrecipitationIntensity { get; set; }
    
        public virtual Place Place { get; set; }
    }
}
