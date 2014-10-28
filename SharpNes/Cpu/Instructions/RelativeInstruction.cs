using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class RelativeInstruction : Instruction
    {
         public RelativeInstruction(Cpu cpu)
            : base(cpu)
        { 
             BaseCycles = 2;
             Destination = new RelativeWordRegisterDestination(Cpu.PC);
         }

        public Func<byte, byte> Op { get; set; }

        public override int Length { get { return 2; } }
        
        public override int Execute(byte [] operands)
        {
            byte value = operands[0];

            byte offset = Op(value);

            Destination.SetByte(offset);

            return BaseCycles;
        }

        public override string PrintInstruction(byte [] operands)
        {
            sbyte value = (sbyte)operands[0];

            return String.Format("{0} *{1}", Mnemonic, value);
        }
    }
}
