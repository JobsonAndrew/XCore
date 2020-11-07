using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XCore {
   public class Instruction : IBinaryble {
      private bool IsImmediate = false;
      private ushort ImmediateValue = 0;
      public enum OperationCode : byte {
         mov,
         add,
         sub,
         mul,
         div,
         and,
         or,
         xor,
         not,
         shr,
         shl
      }
      public OperationCode Operation { get; set; }
      public uint Immediate {
         get {
            return (uint)ImmediateValue;
         }
         set {
            IsImmediate = true;
            ImmediateValue = (ushort)value;
         }
      }

      public Instruction() {

      }

      public void FromBin(Stream stream) {
         throw new NotImplementedException();
      }

      public void ToBin(Stream stream) {
         BinaryWriter bw = new BinaryWriter(stream);


      }
   }
}
