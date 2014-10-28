using System;
using SharpNes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SharpNes.Test
{
    [TestClass]
    public class StatusRegisterTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Status = new StatusRegister();
        }

        protected StatusRegister Status { get; set; }

        [TestMethod]
        public void TestCarrySet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Carry);

            Status.Carry = true;

            Assert.IsTrue(Status.Carry);
            Assert.AreEqual(1, Status.GetByte());

            Status.Carry = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Carry);
        }

        [TestMethod]
        public void TestZeroSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Zero);

            Status.Zero = true;

            Assert.IsTrue(Status.Zero);
            Assert.AreEqual(2, Status.GetByte());

            Status.Zero = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Zero);
        }

        [TestMethod]
        public void TestInterrputDisableSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.InterruptDisable);

            Status.InterruptDisable = true;

            Assert.IsTrue(Status.InterruptDisable);
            Assert.AreEqual(4, Status.GetByte());

            Status.InterruptDisable = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.InterruptDisable);
        }

        [TestMethod]
        public void TestDecimalModeSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.DecimalMode);

            Status.DecimalMode = true;

            Assert.IsTrue(Status.DecimalMode);
            Assert.AreEqual(8, Status.GetByte());

            Status.DecimalMode = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.DecimalMode);
        }

        [TestMethod]
        public void TestBreakSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Break);

            Status.Break = true;

            Assert.IsTrue(Status.Break);
            Assert.AreEqual(16, Status.GetByte());

            Status.Break = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Break);
        }

        [TestMethod]
        public void TestUnusedSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Unused);

            Status.Unused = true;

            Assert.IsTrue(Status.Unused);
            Assert.AreEqual(32, Status.GetByte());

            Status.Unused = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Unused);
        }

        [TestMethod]
        public void TestOverflowSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Overflow);

            Status.Overflow = true;

            Assert.IsTrue(Status.Overflow);
            Assert.AreEqual(64, Status.GetByte());

            Status.Overflow = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Overflow);
        }

        [TestMethod]
        public void TestNegativeSet()
        {
            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Sign);

            Status.Sign = true;

            Assert.IsTrue(Status.Sign);
            Assert.AreEqual(128, Status.GetByte());

            Status.Sign = false;

            Assert.AreEqual(0, Status.GetByte());
            Assert.IsFalse(Status.Sign);
        }
    }
}
