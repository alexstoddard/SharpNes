using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class ZeroPageInstruction : Instruction
    {
        public ZeroPageInstruction(Cpu cpu)
            : base(cpu)
        {
            Destination = new MemoryDestination(cpu);
            BaseCycles = 3; 
        }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            byte next = operands[0];

            byte value = Cpu.GetMemoryByte(next);

            byte result = Op(value);
            
            Destination.Address = next;
            Destination.SetByte(result);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte next = operands[0];

            return String.Format("{0} ${1:X02}", Mnemonic, next);
        }
    }
}
