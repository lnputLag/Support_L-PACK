using Support_L_PACK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_L_PACK.Factory
{
    public abstract class CartridgeCreator
    {
        public abstract ICartridge Create(string model);
    }
}
