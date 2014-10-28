using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class IndexedIndirectInstruction : Instruction
    {
        public IndexedIndirectInstruction(Cpu cpu, ByteRegister register)
            : base(cpu)
        {
            Register = register;
            BaseCycles = 4;
            Destination = new MemoryDestination(cpu);
        }
        
        public ByteRegister Register { get; protected set; }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            byte based = operands[0];
            byte index = Register.GetByte();
            int offset = (index + based) % 256;

            byte valueLow = Cpu.GetMemoryByte(offset);
            byte valueHigh = Cpu.GetMemoryByte(offset + 1);

            int valueOffset = (valueHigh << 8) + valueLow;
            
            byte value = Cpu.GetMemoryByte(valueOffset);

            value = Op(value);
            
            Destination.Address = valueOffset;
            Destination.SetByte(value);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte based = operands[0];

            return String.Format("{0} (${1:X02}, {2})", Mnemonic, based, Register.Name);
        }
    }
}
