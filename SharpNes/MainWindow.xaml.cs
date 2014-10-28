using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SharpNes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Cpu cpu = new Cpu();
            
            sbyte neg = -0x04;

            byte[][] instructions = 
            {
                new byte [] { 0xA9, 0x01},       // LDA #$01
                new byte [] { 0x69, 0x01},       // ADC #$01
                new byte [] { 0xD0, (byte)neg }, // BNE *-4
                new byte [] { 0x80}              // Halt
            };

            byte [] allBytes = instructions.SelectMany(ins => ins.Select(b => b)).ToArray();
            cpu.SetMemoryRange(0, allBytes);

            cpu.SetMemoryByte(0xF0, 11);
            cpu.PC.SetWord(0);

               cpu.Run();

            DataContext = cpu.PrintProgram(0, 0xFF) + "\nA = " + cpu.A.GetByte();
        }
    }
}
