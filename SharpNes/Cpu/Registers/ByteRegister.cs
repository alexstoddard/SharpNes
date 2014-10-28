using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class ByteRegister : Register, IDestination
    {
        protected override bool GetBit(int bit)
        {
            Debug.Assert(bit >= 0 && bit <= 7);

            byte b = GetByte();

            byte mask = 0x1;

            return ((mask << bit) & b) != 0;
        }

        protected override void SetBit(int bit, bool value)
        {
            Debug.Assert(bit >= 0 && bit <= 7);

            byte b = GetByte();
            byte byteValue = (byte)(value ? 0x1 : 0x0);
            byte flag = 0x1;
            byte set = 0xFF;
            byte mask = (byte)(set ^ (flag << bit));

            b = (byte)((mask & b) | (byteValue << bit));

            SetByte(b);
        }

        private byte m_byte;

        public virtual byte GetByte()
        {
            return m_byte;
        }

        public virtual void SetByte(byte value)
        {
            m_byte = value;
        }

        public virtual void SetWord(int value)
        {
            throw new NotImplementedException();
        }

        public int Address { get; set; }
    }
}
