using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class ImpliedInstruction : Instruction
    {
        public ImpliedInstruction(Cpu cpu)
            : base(cpu)
        { 
            BaseCycles = 2;
            Destination = Cpu.A;
        }

        public Action Op { get; set; }

        public override int Length { get { return 1; } }

        public override int Execute(byte [] operands)
        {
            Debug.Assert(operands.Length == 0);

            Op();

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            Debug.Assert(operands.Length == 0);

            return String.Format("{0}", Mnemonic);
        }
    }
}
