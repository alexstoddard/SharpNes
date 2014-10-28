using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class IndirectIndexedInstruction : Instruction
    {
        public IndirectIndexedInstruction(Cpu cpu, ByteRegister register)
            : base(cpu)
        {
            Register = register;
            BaseCycles = 4;
            Destination = Register;
        }
        
        public ByteRegister Register { get; protected set; }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            byte based = operands[0];

            byte valueLow = Cpu.GetMemoryByte(based);
            byte valueHigh = Cpu.GetMemoryByte(based + 1);

            int valueOffset = (valueHigh << 8) + valueLow + Register.GetByte();
            
            byte value = Cpu.GetMemoryByte(valueOffset);

            value = Op(value);

            Destination.Address = valueOffset;
            Destination.SetByte(value);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte based = operands[0];

            return String.Format("{0} (${1:X02}), {2}", Mnemonic, based, Register.Name);
        }
    }
}
