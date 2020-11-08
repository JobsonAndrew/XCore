using System;
using System.IO;
using System.Linq.Expressions;

namespace XCore {
   class Program {
      static Core CPU = new Core();

      static void DrawRegisters() {
         Console.WriteLine("|========================================REGISTERS========================================|");
         int index = 0;
         for (int j = 0; j < 4; j++) {
            Console.Write("|");
            for (int i = 0; i < 4; i++) {
               if (i == 3) {
                  Console.Write($"R{index}\t0x{CPU.REG[index++]:X8}|\n");
               }
               else {
                  Console.Write($"R{index}\t0x{CPU.REG[index++]:X8}\t");
               }
            }
         }
         Console.WriteLine("|===========================================END===========================================|");

      }
      static void Main(string[] args) {
         DrawRegisters();
         Console.ReadKey();

      }
   }
}
