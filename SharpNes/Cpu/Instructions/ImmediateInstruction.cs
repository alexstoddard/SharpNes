using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    class ImmediateInstruction : Instruction
    {
         public ImmediateInstruction(Cpu cpu, ByteRegister register)
            : base(cpu)
        {
            Register = register;
            Destination = Register;
            BaseCycles = 2; 
        }

        public Func<byte, byte> Op { get; set; }

        protected ByteRegister Register { get; set; }

        public override int Length { get { return 2; } }

        public override int Execute(byte [] operands)
        {
            byte value = operands[0];

            byte result = Op(value);

            Destination.SetByte(result);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            byte value = operands[0];

            return String.Format("{0} #${1:X02}", Mnemonic, value);
        }
    }
}
