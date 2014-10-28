using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class VoidDestination : IDestination
    {
        public int Address { get; set; }

        public void SetByte(byte value) { }

        public void SetWord(int value) { }
    }
}
