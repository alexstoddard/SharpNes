using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class RelativeWordRegisterDestination : IDestination
    {
        public RelativeWordRegisterDestination(WordRegister register)
        {
            Register = register;
        }

        protected WordRegister Register { get; set; }

        public int Address { get; set; }

        public void SetByte(byte value)
        {
            sbyte signed = (sbyte)value;
            int current = Register.GetWord();

            current += signed;

            Register.SetWord(current);
        }

        public void SetWord(int value)
        {
            throw new NotImplementedException();
        }
    }
}
