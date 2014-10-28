using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class WordRegister : Register, IDestination
    {
        protected override bool GetBit(int bit)
        {
            Debug.Assert(bit >= 0 && bit <= 15);

            int w = GetWord();

            int mask = 0x1;

            return ((mask << bit) & w) != 0;
        }

        protected override void SetBit(int bit, bool value)
        {
            Debug.Assert(bit >= 0 && bit <= 15);

            int w = GetWord();
            int byteValue = (value ? 0x1 : 0x0);
            int flag = 0x1;
            int set = 0xFF;
            int mask = (set ^ (flag << bit));

            w = (byte)((mask & w) | (byteValue << bit));

            SetWord(w);
        }

        private int m_word;

        public virtual int GetWord()
        {
            return m_word;
        }

        public virtual void SetByte(byte value)
        {
            throw new NotImplementedException();
        }

        public virtual void SetWord(int value)
        {
            m_word = value;
        }

        public int Address { get; set; }
    }
}
