using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XCore
{

    public class Core
    {
        public uint[] Register = new uint[16];

        public MemoryStream Memory;
        private BinaryWriter SRAMIn;
        private BinaryReader SRAMOut;

        public uint PC
        {
            get
            {
                return Register[15];
            }
            set
            {
                Register[15] = value;
            }
        }
        public uint IC
        {
            get
            {
                return Register[14];
            }
            set
            {
                Register[14] = value;
            }
        }
        public uint SP
        {
            get
            {
                return Register[13];
            }
            set
            {
                Register[13] = value;
            }
        }

        private List<InstructionContainer> Instructions = new List<InstructionContainer>();

        public Core(int capacity = 8192)
        {
            Memory = new MemoryStream(capacity);
            SRAMIn = new BinaryWriter(Memory);
            SRAMOut = new BinaryReader(Memory);
        }

        public void AddInstruction(InstructionContainer ins)
        {
            Instructions.Add(ins);
        }

        public void Reset()
        {

        }
        
        private void sdb(uint address, uint data)
        {
            SRAMIn.Seek((int)address, SeekOrigin.Begin);
            SRAMIn.Write((byte)data);
            SRAMIn.Flush();
        }
        private void sds(uint address, uint data)
        {
            SRAMIn.Seek((int)address, SeekOrigin.Begin);
            SRAMIn.Write((short)data);
            SRAMIn.Flush();
        }
        private void sdw(uint address, uint data)
        {
            SRAMIn.Seek((int)address, SeekOrigin.Begin);
            SRAMIn.Write((uint)data);
            SRAMIn.Flush();
        }
        private uint ldb(uint address)
        {
            SRAMOut.BaseStream.Seek(address, SeekOrigin.Begin);
            return (uint)SRAMOut.ReadByte();
        }
        private uint lds(uint address)
        {
            SRAMOut.BaseStream.Seek(address, SeekOrigin.Begin);
            return (uint)SRAMOut.ReadUInt16();
        }
        private uint ldw(uint address)
        {
            SRAMOut.BaseStream.Seek(address, SeekOrigin.Begin);
            return (uint)SRAMOut.ReadUInt32();
        }


    }
}
