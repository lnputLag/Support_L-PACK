using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_L_PACK.Models
{
    public class ColorCartridge : ICartridge
    {
        public string Model { get; set; }
        public string Type => "Color";

        public string GetInfo() => $"Цветной картридж: {Model}";

        public string Info => GetInfo();
    }
}
