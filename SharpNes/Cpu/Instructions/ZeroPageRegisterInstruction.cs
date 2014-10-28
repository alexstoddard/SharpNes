using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class ZeroPageRegisterInstruction : Instruction
    {
         public ZeroPageRegisterInstruction(Cpu cpu, ByteRegister register)
            : base(cpu)
        {
            Register = register;
            BaseCycles = 4;
            Destination = new MemoryDestination(Cpu);
        }

        public ByteRegister Register { get; protected set; }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            Debug.Assert(operands.Length == 1);

            byte based = operands[0];

            byte index = Register.GetByte();
            int offset = (index + based) % 256;
            byte value = Cpu.GetMemoryByte(offset);

            value = Op(value);

            Destination.Address = offset;
            Destination.SetByte(value);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            Debug.Assert(operands.Length == 1);

            byte next = Cpu.GetMemoryByte(operands[0]);

            return String.Format("{0} ${1:X02}, {2}", Mnemonic, next, Register.Name);
        }
    }
}
