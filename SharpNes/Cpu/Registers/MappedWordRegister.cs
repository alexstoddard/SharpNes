using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class MappedWordRegister : WordRegister
    {
        public MappedWordRegister(Cpu cpu, byte low, byte high)
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
        private MappedWordRegister() { }

        public override int GetWord()
        {
            return Cpu.GetMemoryWord(((High << 8) + Low));
        }

        public override void SetWord(int value)
        {
            Cpu.SetMemoryWord(((High << 8) + Low), value);
        }
    }
}
