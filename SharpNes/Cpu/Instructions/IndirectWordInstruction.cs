using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class IndirectWordInstruction : Instruction
    {
         public IndirectWordInstruction(Cpu cpu)
            : base(cpu)
        { 
             BaseCycles = 6;
             Destination = Cpu.PC;
        }

        public Func<int, int> Op { get; set; }

        public override int Length { get { return 3; } }
        
        public override int Execute(byte [] operands)
        {
            byte low = operands[0];
            byte high = operands[1];
            
            //TODO: Make sure that we wrap around if crossing page boundary
            int offset = (high << 8) + low;

            byte valueLow = Cpu.GetMemoryByte(offset);
            byte valueHigh = Cpu.GetMemoryByte(offset + 1);

            int realOffset = (valueHigh << 8) + valueLow;
            int result = Op(realOffset);

            if (result != realOffset)
            {
                Destination.Address = offset;
                Destination.SetWord(result);
            }

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte low = operands[0];
            byte high = operands[1];

            int offset = (high << 8) + low;

            return String.Format("{0} (${1:X04})", Mnemonic, offset);
        }
    }
}
