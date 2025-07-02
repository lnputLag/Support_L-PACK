using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_L_PACK.Models
{
    public interface ICartridge
    {
        string Model { get; set; }
        string Type { get; }
        string GetInfo();
        string Info { get; }
    }
}
