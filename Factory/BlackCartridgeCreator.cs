using Support_L_PACK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Support_L_PACK.Factory
{
    public class BlackCartridgeCreator : CartridgeCreator
    {
        public override ICartridge Create(string model)
        {
            return new BlackCartridge { Model = model };
        }
    }
}
