using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class AbsoluteRegisterInstruction : Instruction
    {
        
        public AbsoluteRegisterInstruction(Cpu cpu, ByteRegister register)
            : base(cpu)
        {
            Register = register;
            BaseCycles = 4;
            Destination = new MemoryDestination(cpu);
        }
        
        public ByteRegister Register { get; protected set; }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 3; } }
        
        public override int Execute(byte [] operands)
        {
            byte low = operands[0];
            byte high = operands[1];
            byte index = Register.GetByte();
            
            int offset = (high << 8) + low + index;

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

            return String.Format("{0} ${1:X04}, {2}", Mnemonic, offset, Register.Name);
        }
    }
}
