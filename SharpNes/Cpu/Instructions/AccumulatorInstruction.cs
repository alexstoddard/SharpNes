using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class AccumulatorInstruction : Instruction
    {
        public AccumulatorInstruction(Cpu cpu)
            : base(cpu)
        { 
            BaseCycles = 2;
            Destination = Cpu.A;
        }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 1; } }

        public override int Execute(byte [] operands)
        {
            Debug.Assert(operands.Length == 0);

            byte accum = Cpu.A.GetByte();
            byte result = Op(accum);

            Destination.SetByte(result);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            Debug.Assert(operands.Length == 0);

            return String.Format("{0} {1}", Mnemonic, Cpu.A.Name);
        }
    }
}
