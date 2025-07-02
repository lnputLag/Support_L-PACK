using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_L_PACK.Models
{
    public class BlackCartridge : ICartridge
    {
        public string Model { get; set; }
        public string Type => "Black";
        public string GetInfo() => $"Чёрно-белый картридж: {Model}";
        public string Info => GetInfo();
    }
}
