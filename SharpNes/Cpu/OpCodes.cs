using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class OpCodes
    {
        public OpCodes(Cpu cpu)
        {
            Cpu = cpu;
        }

        public Cpu Cpu { get; set; }
        
        public byte ADC(byte source)
        {
            byte accum = Cpu.A.GetByte();

            int total = accum + source + (Cpu.Status.Carry ? 1 : 0);

            Cpu.Status.Overflow = (total & (1 << 7)) != (total & (1 << 7));
            Cpu.Status.Sign = (total & (1 << 7)) != 0;
            Cpu.Status.Zero = total == 0;
            Cpu.Status.Carry = total > 255;

            return ((byte)(total & 0xFF));
        }

        public byte AND(byte source)
        {
            byte accum = Cpu.A.GetByte();

            byte result = (byte)(accum & source);

            Cpu.Status.Sign = (result & (1 << 7)) != 0;
            Cpu.Status.Zero = result == 0;

            return ((byte)(result & 0xFF));
        }

        public byte ASL(byte value)
        {
            Cpu.Status.Carry = (value & 128) != 0;

            byte result = (byte)(value << 1);

            Cpu.Status.Zero = result == 0;
            Cpu.Status.Sign = (result & 128) != 0;

            return result;
        }
        public byte BCC(byte offset)
        {
            return (byte)(Cpu.Status.Carry ? 0 : offset);
        }
        public byte BCS(byte offset)
        {
            return (byte)(Cpu.Status.Carry ? offset : 0);
        }

        public byte BEQ(byte offset)
        {
            return (byte)(Cpu.Status.Zero ? offset : 0);
        }

        public byte BMI(byte offset)
        {
            return (byte)(Cpu.Status.Sign ? offset : 0);
        }

        public byte BNE(byte offset)
        {
            return (byte)(Cpu.Status.Zero ? 0: offset);
        }

        public byte BPL(byte offset)
        {
            return (byte)(Cpu.Status.Sign ? 0 : offset);
        }

        public byte BIT(byte value)
        {
            byte total = (byte)(value & Cpu.A.GetByte());

            Cpu.Status.Sign = (total & (1 << 7)) != 0;
            Cpu.Status.Overflow = (total & (1 << 6)) != 0;
            Cpu.Status.Zero = (total == 0);

            return value;
        }

        public void BRK()
        {
            int pc = Cpu.PC.GetWord();
            byte sp = Cpu.SP.GetByte();

            pc++;

            Cpu.SetMemoryByte(sp, (byte)(pc >> 8));

            sp--;

            Cpu.SetMemoryByte(sp, (byte)(pc & 0xFF));

            sp--;

            Cpu.SetMemoryByte(sp, (byte)(Cpu.Status.GetByte() | 0x10) );

            sp--;

            Cpu.SP.SetByte(sp);

            byte low = Cpu.GetMemoryByte(0xFFFE);
            byte high = Cpu.GetMemoryByte(0xFFFF);

            Cpu.PC.SetWord((high << 8) | low);
        }

        public byte BVC(byte offset)
        {
            return (byte)(Cpu.Status.Overflow ? 0 : offset);
        }

        public byte BVS(byte offset)
        {
            return (byte)(Cpu.Status.Overflow ? offset: 0);
        }

        public void CLC()
        {
            Cpu.Status.Carry = false;
        }

        public void CLD()
        {
            Cpu.Status.DecimalMode = false;
        }

        public int JMP(int value)
        {
            Cpu.PC.SetWord(value);
            return value;
        }

        public byte LDA(byte value)
        {
            return value;
        }
    }
}
