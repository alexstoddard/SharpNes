using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SharpNes
{
    public class MappedByteRegister : ByteRegister
    {
        public MappedByteRegister(Cpu cpu, byte low, byte high)
        {
            Cpu = cpu;
            Low = low;
            High = high;
        }

        protected Cpu Cpu { get; set; }

        protected byte Low { get; set; }

        protected byte High { get; set; }
        /// <summary>
        /// Mapped registers must have access to the Cpu, so can't construct one without a reference
        /// </summary>
        private MappedByteRegister() { }

        public override byte GetByte()
        {
            return Cpu.GetMemoryByte((ushort)((High << 8) + Low));
        }

        public override void SetByte(byte value)
        {
            Cpu.SetMemoryByte((ushort)((High << 8) + Low), value);
        }
    }
}
