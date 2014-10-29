using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpNes
{
    public class Cpu
    {
        public Cpu()
        {
            Ops = new OpCodes(this);

            Status = new StatusRegister() { Name = "Status" };

            SP = new ByteRegister { Name = "SP" };
            X  = new ByteRegister { Name = "X" };
            Y  = new ByteRegister { Name = "Y" };
            A  = new ByteRegister { Name = "A" };
            PC = new WordRegister { Name = "PC" };

            Stack = new byte[256];
            Rom = new byte[1024];

            Instructions = GetInstructions();
        }

        public OpCodes Ops { get; protected set; }

        public StatusRegister Status;
        public ByteRegister X;
        public ByteRegister Y;
        public ByteRegister A;
        public ByteRegister SP;
        public WordRegister PC;

        public byte[] Rom;
        public byte[] Stack;
        public Instruction [] Instructions;
        
        public Instruction [] GetInstructions() 
        {
            MemoryDestination memory = new MemoryDestination(this);
            RelativeWordRegisterDestination relativePC = new RelativeWordRegisterDestination(PC);
            VoidDestination voidDest = new VoidDestination();

            return new Instruction [] 
            {

                // ADC - Add Memory to A with Carry
                new ImmediateInstruction(this, A) {          OpCode=0x69, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 2, Destination = A },
                new ZeroPageInstruction(this) {              OpCode=0x65, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 3, Destination = A },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0x75, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 4, Destination = A },
                new AbsoluteInstruction(this) {              OpCode=0x6D, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0x7D, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, Y) {   OpCode=0x79, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 4, Destination = A },
                new IndexedIndirectInstruction(this, X)  {   OpCode=0x61, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 6, Destination = A },
                new IndirectIndexedInstruction(this, Y)  {   OpCode=0x71, Mnemonic="ADC", Op = Ops.ADC, BaseCycles = 5, Destination = A },
                // AND - Bitwise AND A with Memory
                new ImmediateInstruction(this, A) {          OpCode=0x29, Mnemonic="AND", Op = Ops.AND, BaseCycles = 2, Destination = A },
                new ZeroPageInstruction(this) {              OpCode=0x25, Mnemonic="AND", Op = Ops.AND, BaseCycles = 2, Destination = A },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0x35, Mnemonic="AND", Op = Ops.AND, BaseCycles = 3, Destination = A },
                new AbsoluteInstruction(this) {              OpCode=0x2D, Mnemonic="AND", Op = Ops.AND, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0x3D, Mnemonic="AND", Op = Ops.AND, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, Y) {   OpCode=0x39, Mnemonic="AND", Op = Ops.AND, BaseCycles = 4, Destination = A },
                new IndexedIndirectInstruction(this, X)  {   OpCode=0x21, Mnemonic="AND", Op = Ops.AND, BaseCycles = 6, Destination = A },
                new IndirectIndexedInstruction(this, Y)  {   OpCode=0x31, Mnemonic="AND", Op = Ops.AND, BaseCycles = 5, Destination = A },
                // ASL - Arithmetic Shift Left
                new AccumulatorInstruction(this) {           OpCode=0x0A, Mnemonic="ASL", Op = Ops.ASL, BaseCycles = 2, Destination = A },
                new ZeroPageInstruction(this) {              OpCode=0x06, Mnemonic="ASL", Op = Ops.ASL, BaseCycles = 5, Destination = memory },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0x16, Mnemonic="ASL", Op = Ops.ASL, BaseCycles = 6, Destination = memory },
                new AbsoluteInstruction(this) {              OpCode=0x0E, Mnemonic="ASL", Op = Ops.ASL, BaseCycles = 6, Destination = memory },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0x1E, Mnemonic="ASL", Op = Ops.ASL, BaseCycles = 7, Destination = memory },
                // BCC - Branch if Carry is Clear
                new RelativeInstruction(this) {              OpCode=0x90, Mnemonic="BCC", Op = Ops.BCC, BaseCycles = 2, Destination = relativePC },
                // BCS - Branch if Carry is Set
                new RelativeInstruction(this) {              OpCode=0xB0, Mnemonic="BCC", Op = Ops.BCS, BaseCycles = 2, Destination = relativePC },
                // BEQ - Branch if Zero is Set
                new RelativeInstruction(this) {              OpCode=0xF0, Mnemonic="BEQ", Op = Ops.BEQ, BaseCycles = 2, Destination = relativePC },
                // BIT - Test bits in Accumulator with Memory
                new ZeroPageInstruction(this) {              OpCode=0x24, Mnemonic="BIT", Op = Ops.BIT, BaseCycles = 3, Destination = voidDest },
                new AbsoluteInstruction(this) {              OpCode=0x2C, Mnemonic="BIT", Op = Ops.BIT, BaseCycles = 4, Destination = voidDest },
                // BMI - Branch if Sign is Set
                new RelativeInstruction(this) {              OpCode=0x30, Mnemonic="BMI", Op = Ops.BMI, BaseCycles = 2, Destination = relativePC },
                // BNE - Branch if Zero is Clear
                new RelativeInstruction(this) {              OpCode=0xD0, Mnemonic="BNE", Op = Ops.BNE, BaseCycles = 2, Destination = relativePC },
                // BPL - Branch if Sign is Clear
                new RelativeInstruction(this) {              OpCode=0x10, Mnemonic="BPL", Op = Ops.BPL, BaseCycles = 2, Destination = relativePC },
                // BRK - Simulate IRQ
                new ImpliedInstruction(this) {               OpCode=0x00, Mnemonic="BRK", Op = Ops.BRK, BaseCycles = 7, Destination = voidDest },
                // BVC - Branch if Overflow is Clear
                new RelativeInstruction(this) {              OpCode=0x50, Mnemonic="BVC", Op = Ops.BVC, BaseCycles = 2, Destination = relativePC },
                // BVS - Branch if Overflow is Set
                new RelativeInstruction(this) {              OpCode=0x70, Mnemonic="BVS", Op = Ops.BVS, BaseCycles = 2, Destination = relativePC },
                // CLC - Clear Carry Flag
                new ImpliedInstruction(this) {               OpCode=0x18, Mnemonic="CLC", Op = Ops.CLC, BaseCycles = 2, Destination = voidDest },
                // CLD - Clear Decimal Flag
                new ImpliedInstruction(this) {               OpCode=0xD8, Mnemonic="CLD", Op = Ops.CLD, BaseCycles = 2, Destination = voidDest },
                // CLI - Clear Decimal Flag
                new ImpliedInstruction(this) {               OpCode=0xD8, Mnemonic="CLI", Op = Ops.CLI, BaseCycles = 2, Destination = voidDest },
                // CLV - Clear Decimal Flag
                new ImpliedInstruction(this) {               OpCode=0xD8, Mnemonic="CLV", Op = Ops.CLV, BaseCycles = 2, Destination = voidDest },
                // CMP - Compare Accumulator With Memory
                new ImmediateInstruction(this, A) {          OpCode=0xC9, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 2, Destination = voidDest },
                new ZeroPageInstruction(this) {              OpCode=0xC5, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 2, Destination = voidDest },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0xD5, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 4, Destination = voidDest },
                new AbsoluteInstruction(this) {              OpCode=0xCD, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 4, Destination = voidDest },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0xDD, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 4, Destination = voidDest },
                new AbsoluteRegisterInstruction(this, Y) {   OpCode=0xD9, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 4, Destination = voidDest },
                new IndexedIndirectInstruction(this, X)  {   OpCode=0xC1, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 6, Destination = voidDest },
                new IndirectIndexedInstruction(this, Y)  {   OpCode=0xD1, Mnemonic="CMP", Op = Ops.CMP, BaseCycles = 5, Destination = voidDest },
                // CPX - Compare X With Memory
                new ImmediateInstruction(this, A) {          OpCode=0xE0, Mnemonic="CPX", Op = Ops.CPX, BaseCycles = 2, Destination = voidDest },
                new ZeroPageInstruction(this) {              OpCode=0xE4, Mnemonic="CPX", Op = Ops.CPX, BaseCycles = 3, Destination = voidDest },
                new AbsoluteInstruction(this) {              OpCode=0xEC, Mnemonic="CPX", Op = Ops.CPX, BaseCycles = 4, Destination = voidDest },
                // CPY - Compare Y With Memory
                new ImmediateInstruction(this, A) {          OpCode=0xC0, Mnemonic="CPY", Op = Ops.CPY, BaseCycles = 2, Destination = voidDest },
                new ZeroPageInstruction(this) {              OpCode=0xC4, Mnemonic="CPY", Op = Ops.CPY, BaseCycles = 3, Destination = voidDest },
                new AbsoluteInstruction(this) {              OpCode=0xCC, Mnemonic="CPY", Op = Ops.CPY, BaseCycles = 4, Destination = voidDest },
                // DEC - Decrement Memory by one
                new ZeroPageInstruction(this) {              OpCode=0xC6, Mnemonic="DEC", Op = Ops.DEC, BaseCycles = 5, Destination = memory },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0xD6, Mnemonic="DEC", Op = Ops.DEC, BaseCycles = 6, Destination = memory },
                new AbsoluteInstruction(this) {              OpCode=0xCE, Mnemonic="DEC", Op = Ops.DEC, BaseCycles = 6, Destination = memory },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0xDE, Mnemonic="DEC", Op = Ops.DEC, BaseCycles = 7, Destination = memory },
                // DEX - Decrement X by one
                new ImpliedInstruction(this) {               OpCode=0xCA, Mnemonic="DEX", Op = Ops.DEX, BaseCycles = 2, Destination = X },
                // DEY - Decrement Y by one
                new ImpliedInstruction(this) {               OpCode=0x88, Mnemonic="DEY", Op = Ops.DEY, BaseCycles = 2, Destination = Y },
                // EOR - Exclusive or Accumulator with Memory
                new ImmediateInstruction(this, A) {          OpCode=0x49, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 2, Destination = A },
                new ZeroPageInstruction(this) {              OpCode=0x45, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 3, Destination = A },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0x55, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 4, Destination = A },
                new AbsoluteInstruction(this) {              OpCode=0x4D, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0x5D, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, Y) {   OpCode=0x59, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 4, Destination = A },
                new IndexedIndirectInstruction(this, X)  {   OpCode=0x41, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 6, Destination = A },
                new IndirectIndexedInstruction(this, Y)  {   OpCode=0x51, Mnemonic="EOR", Op = Ops.EOR, BaseCycles = 5, Destination = A },
                // INC - Increment Memory by one
                new ZeroPageInstruction(this) {              OpCode=0xE6, Mnemonic="INC", Op = Ops.INC, BaseCycles = 5, Destination = memory },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0xF6, Mnemonic="INC", Op = Ops.INC, BaseCycles = 6, Destination = memory },
                new AbsoluteInstruction(this) {              OpCode=0xEE, Mnemonic="INC", Op = Ops.INC, BaseCycles = 6, Destination = memory },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0xFE, Mnemonic="INC", Op = Ops.INC, BaseCycles = 7, Destination = memory },
                // INX - Increment X by one
                new ImpliedInstruction(this) {               OpCode=0xE8, Mnemonic="INX", Op = Ops.INX, BaseCycles = 2, Destination = X },
                // INY - Increment Y by one
                new ImpliedInstruction(this) {               OpCode=0xC8, Mnemonic="INY", Op = Ops.INY, BaseCycles = 2, Destination = Y },
                // JMP - Jump
                new AbsoluteWordInstruction(this)        {   OpCode=0x4C, Mnemonic="JMP", Op = Ops.JMP, BaseCycles = 3, Destination = PC },
                new IndirectWordInstruction(this)        {   OpCode=0x6C, Mnemonic="JMP", Op = Ops.JMP, BaseCycles = 5, Destination = PC },
                // JSR - Jump to Subroutine
                new AbsoluteWordInstruction(this) {          OpCode=0x20, Mnemonic="JSR", Op = Ops.JSR, BaseCycles = 6, Destination = PC },
                // LDA - Load Accumulator with Memory
                new ImmediateInstruction(this, A) {          OpCode=0xA9, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 2, Destination = A },
                new ZeroPageInstruction(this) {              OpCode=0xA5, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 3, Destination = A },
                new ZeroPageRegisterInstruction(this, X) {   OpCode=0xB5, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 4, Destination = A },
                new AbsoluteInstruction(this) {              OpCode=0xAD, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, X) {   OpCode=0xBD, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 4, Destination = A },
                new AbsoluteRegisterInstruction(this, Y) {   OpCode=0xB9, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 4, Destination = A },
                new IndexedIndirectInstruction(this, X)  {   OpCode=0xA1, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 6, Destination = A },
                new IndirectIndexedInstruction(this, Y)  {   OpCode=0xB1, Mnemonic="LDA", Op = Ops.LDA, BaseCycles = 5, Destination = A },
            };
        }

        public byte GetMemoryByte(int address)
        {
            return Rom[address];
        }

        public void SetMemoryByte(int address, byte value)
        {
            Rom[address] = value;
        }

        public byte [] GetMemoryRange(int address, int count)
        {
            byte[] data = new byte[count];
            for (int i = 0; i < count; i++)
            {
                data[i] = Rom[address+i];;
            }

            return data;
        }

        public void SetMemoryRange(int address, byte[] value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                Rom[address+i] = value[i];
            }
        }

        public short GetMemoryWord(int address)
        {
            return 0;
        }

        public void SetMemoryWord(int address, int value)
        {

        }

        public void Run()
        {
            bool operational = false;

            Dictionary<byte, Instruction> instructions = Instructions.ToDictionary(instruction => instruction.OpCode, instruction => instruction);
            int count = 0;

            do
            {
                count++;

                int pc = PC.GetWord();
                byte opCode = GetMemoryByte(pc);

                operational = instructions.ContainsKey(opCode);

                if (operational)
                {
                    Instruction i = instructions[opCode];

                    byte[] operands = GetMemoryRange(pc + 1, i.Length - 1);
                    pc += i.Length;
                    PC.SetWord(pc);

                    i.Execute(operands);
                }

            } while (operational);
        }

        public string PrintProgram(int addressLow, int addressHigh)
        {
            bool operational = false;
            int pc = addressLow;

            Dictionary<byte, Instruction> instructions = Instructions.ToDictionary(instruction => instruction.OpCode, instruction => instruction);
            List<string> lines = new List<string>();

            do
            {
                byte opCode = GetMemoryByte(pc);

                operational = instructions.ContainsKey(opCode);

                if (operational)
                {
                    Instruction i = instructions[opCode];

                    byte[] operands = GetMemoryRange(pc + 1, i.Length - 1);

                    string printedInstruction = i.PrintInstruction(operands);
                    lines.Add(printedInstruction);

                    pc += i.Length;
                }

            } while (operational && pc < addressHigh);

            return string.Join(Environment.NewLine, lines);
        }
    }
}
