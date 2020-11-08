using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XCore
{
    public class Instruction : IBinaryble
    {
        public string Label { get; set; } = "";
        public bool IsLabel
        {
            get
            {
                if (Label.Length == 0) return false;
                else return true;
            }
        }
        public uint Address { get; set; }
        public int Size
        {
            get
            {
                if (IsImmediate) return 4;
                else return 2;
            }
        }
        private bool IsImmediate = false;
        private ushort ImmediateValue = 0;
        public enum OperationCode : ushort
        {
            mov,
            add,
            adc,
            sub,
            mul,
            div,
            and,
            or,
            xor,
            not,
            shr,
            shl,
            ldb,
            lds,
            ldw,
            stb,
            sts,
            stw,
            sifl,
            sifle,
            sifne,
            sife
        }
        public OperationCode Operation { get; set; }
        public uint Rd { get; set; }
        public uint Rs { get; set; }
        public uint Imm
        {
            get
            {
                return (uint)ImmediateValue;
            }
            set
            {
                IsImmediate = true;
                ImmediateValue = (ushort)value;
            }
        }
        public Instruction()
        {

        }
        public void FromBin(Stream stream)
        {
            BinaryReader br = new BinaryReader(stream);
            ushort ins = br.ReadUInt16();
            if ((ins & 0x8000) != 0)
            {
                Imm = (uint)br.ReadUInt16();
            }
            Operation = (OperationCode)((ins & ~0x8000) >> 8);
            Rd = (uint)(ins & 0x00F0) >> 4;
            Rs = (uint)(ins & 0x000F);
        }
        public void ToBin(Stream stream)
        {
            BinaryWriter bw = new BinaryWriter(stream);
            ushort ins = 0;
            if (IsImmediate)
            {
                ins |= 0x8000;
            }
            ins |= (ushort)((ushort)Operation << 8);
            ins |= (ushort)((ushort)Rd << 4);
            ins |= (ushort)((ushort)Rs << 0);
            bw.Write(ins);
            if (IsImmediate)
            {
                bw.Write((ushort)Imm);
            }
            bw.Flush();

        }
        public override string ToString()
        {
            MemoryStream ms = new MemoryStream();
            string str = "";
            this.ToBin(ms);
            if (IsLabel) str = $"@{Label}:";
            str += $"\t\t\t\t\t\t{Address:X6}h: ";
            foreach (byte x in ms.ToArray())
                str += $"{x:X2}";
            if (IsImmediate) str += $"h\t{Operation} R{Rd}, R{Rs} + {Imm};";
            else str += $"h\t\t{Operation} R{Rd}, R{Rs};";
            return str;
        }
        public void FromString(string str)
        {
            

        }
    }
}
