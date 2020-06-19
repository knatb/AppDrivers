using System;
using System.Collections.Generic;
using System.Text;

namespace AppDrivers.Models
{
    public class DriverModel
    {
        //[JsonProperty("ID")]
        public int IDDriver { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Status { get; set; }
        public PositionModel ActualPosition { get; set; }
    }
}
