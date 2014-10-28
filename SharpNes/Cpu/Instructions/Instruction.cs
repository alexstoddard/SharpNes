using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public abstract class Instruction
    {
        public Instruction(Cpu cpu)
        {
            Cpu = cpu;
        }

        protected Cpu Cpu { get; set; }

        public string Mnemonic { get; set; }

        public abstract string PrintInstruction(byte [] operands);

        public byte OpCode { get; set; }
        public IDestination Destination { get; set; }

        public int BaseCycles { get; set; }
        public abstract int Length { get;  }
        public abstract int Execute(byte [] operands);
    }
}
