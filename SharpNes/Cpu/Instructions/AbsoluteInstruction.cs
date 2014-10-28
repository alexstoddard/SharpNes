using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class AbsoluteInstruction : Instruction
    {
        public AbsoluteInstruction(Cpu cpu)
            : base(cpu)
        { 
            BaseCycles = 4;
            Destination = new MemoryDestination(cpu);
        }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            byte low = operands[0];
            byte high = operands[1];
            
            int offset = (high << 8) + low;

            byte value = Cpu.GetMemoryByte(offset);

            value = Op(value);

            Destination.Address = offset;
            Destination.SetByte(value);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte low = operands[0];
            byte high = operands[1];

            int offset = (high << 8) + low;
            
            return String.Format("{0} ${1:X04}", Mnemonic, offset);
        }
    }
}
