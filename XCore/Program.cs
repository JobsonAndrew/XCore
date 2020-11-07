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
            ms.Position = 0;
            ins2.FromBin(ms);
            Console.WriteLine(ins);
            ins2.Label = "_loop0";
            Console.WriteLine(ins2);
        }
    }
}
