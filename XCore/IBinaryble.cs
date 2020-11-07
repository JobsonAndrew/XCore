using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XCore {
   public interface IBinaryble {
      void FromBin(Stream stream);
      void ToBin(Stream stream);
   }
}
