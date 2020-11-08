using System;
using System.IO;

namespace XCore
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStream ms = new MemoryStream();
            Instruction ins = new Instruction();
            ins.Operation = Instruction.OperationCode.shl;
            ins.Rd = 1;
            ins.Rs = 0;
            ins.Imm = 4;
            ins.ToBin(ms);
            Instruction ins2 = new Instruction();
            Instruction ins3 = new Instruction();
            ins3.Rd = 3;
            ins3.Rs = 1;
            ins3.Operation = Instruction.OperationCode.mul;
            ms.Position = 0;
            ins2.FromBin(ms);
            Console.WriteLine(ins);
            ins2.Label = "loop0";
            Console.WriteLine(ins2);
            Console.WriteLine(ins3);
            StreamWriter sw = File.CreateText("out.asm");
            sw.WriteLine(ins);
            sw.WriteLine(ins2);
            sw.WriteLine(ins3);
            sw.Close();

        }
    }
}
