using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public abstract class Register
    {
        protected abstract bool GetBit(int bit);

        protected abstract void SetBit(int bit, bool value);

        public string Name { get; set; }
    }
}
