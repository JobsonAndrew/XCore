using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XCore {
   public class Instruction : IBinaryble {
      private bool IsImmediate = false;
      private UInt16 ImmediateValue = 0;
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
      public UInt16 Immediate {
         get {
            return ImmediateValue;
         }
         set {
            IsImmediate = true;
            ImmediateValue = value;
         }
      }

      public Instruction() {

      }

      public void FromBin(Stream[] bin) {
         throw new NotImplementedException();
      }

      public void ToBin(Stream[] bin) {
         throw new NotImplementedException();
      }
   }
}
