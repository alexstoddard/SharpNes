using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public interface IDestination
    {
        int Address { get; set; }

        void SetByte(byte value);

        void SetWord(int value);
    }

    public class MemoryDestination : IDestination
    {
        public MemoryDestination(Cpu cpu)
        {
            Cpu = cpu;
        }

        protected Cpu Cpu { get; set; }

        public int Address { get; set; }

        public void SetByte(byte value)
        {
            Cpu.SetMemoryByte(Address, value);
        }

        public void SetWord(int value)
        {

        }
    }
}
