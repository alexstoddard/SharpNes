using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpNes.Test
{
    [TestClass]
    public class ADC_Test
    {
        [TestInitialize]
        public void Initialize()
        {
            Cpu = new Cpu();
        }

        protected Cpu Cpu { get; set; }

        [TestMethod]
        public void ZeroPlusOne()
        {
            Assert.AreEqual(0, Cpu.A.GetByte());

            Cpu.A.SetByte(Cpu.Ops.ADC(1));

            Assert.AreEqual(1, Cpu.A.GetByte());

            Assert.IsFalse(Cpu.Status.Carry);
            Assert.IsFalse(Cpu.Status.Sign);
            Assert.IsFalse(Cpu.Status.Overflow);
            Assert.IsFalse(Cpu.Status.Zero);
        }

        [TestMethod]
        public void ZeroPlusZero()
        {
            Assert.AreEqual(0, Cpu.A.GetByte());

            Cpu.A.SetByte(Cpu.Ops.ADC(0));

            Assert.AreEqual(0, Cpu.A.GetByte());

            Assert.IsFalse(Cpu.Status.Carry);
            Assert.IsFalse(Cpu.Status.Sign);
            Assert.IsFalse(Cpu.Status.Overflow);
            Assert.IsTrue(Cpu.Status.Zero);
        }

        [TestMethod]
        public void MaxPlusOne()
        {
            Cpu.A.SetByte(0xFF);

            Assert.AreEqual(0xFF, Cpu.A.GetByte());

            Cpu.A.SetByte(Cpu.Ops.ADC(1));

            Assert.AreEqual(0, Cpu.A.GetByte());

            Assert.IsTrue(Cpu.Status.Carry);
            Assert.IsFalse(Cpu.Status.Sign);
            Assert.IsTrue(Cpu.Status.Overflow);
            Assert.IsFalse(Cpu.Status.Zero);
        }
    }
}
