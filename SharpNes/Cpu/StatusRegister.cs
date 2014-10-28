using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class StatusRegister : ByteRegister
    {
        public bool Carry
        {
            get { return GetBit(0); }
            set { SetBit(0, value); }
        }
        public bool Zero
        {
            get { return GetBit(1); }
            set { SetBit(1, value); }
        }
        public bool InterruptDisable
        {
            get { return GetBit(2); }
            set { SetBit(2, value); }
        }
        public bool DecimalMode
        {
            get { return GetBit(3); }
            set { SetBit(3, value); }
        }
        public bool Break
        {
            get { return GetBit(4); }
            set { SetBit(4, value); }
        }
        public bool Unused
        {
            get { return GetBit(5); }
            set { SetBit(5, value); }
        }
        public bool Overflow
        {
            get { return GetBit(6); }
            set { SetBit(6, value); }
        }
        public bool Sign
        {
            get { return GetBit(7); }
            set { SetBit(7, value); }
        }
    }
}
