using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XCore
{

    public class Core
    {
        public uint[] REG = new uint[16];

        public MemoryStream Memory;
        private BinaryWriter SRAMIn;
        private BinaryReader SRAMOut;

        public uint PC
        {
            get
            {
                return REG[15];
            }
            set
            {
                REG[15] = value;
            }
        }
        public uint IC
        {
            get
            {
                return REG[14];
            }
            set
            {
                REG[14] = value;
            }
        }
        public uint SP
        {
            get
            {
                return REG[13];
            }
            set
            {
                REG[13] = value;
            }
        }

        public uint Carry { get; set; }
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

        public void Execute(InstructionContainer ins)
        {
            switch (ins.Operation)
            {
                case Operation.mov:
                    REG[ins.Rd] = REG[ins.Rs] + ins.Imm;
                    break;
                case Operation.add:
                    REG[ins.Rd] = REG[ins.Rd] + REG[ins.Rs] + ins.Imm;
                    break;
                case Operation.adc:
                    REG[ins.Rd] = REG[ins.Rd] + REG[ins.Rs] + Carry + ins.Imm;
                    break;
                case Operation.sub:
                    REG[ins.Rd] = REG[ins.Rd] - (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.mul:
                    REG[ins.Rd] = REG[ins.Rd] * (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.div:
                    REG[ins.Rd] = REG[ins.Rd] / (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.and:
                    REG[ins.Rd] = REG[ins.Rd] & (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.or:
                    REG[ins.Rd] = REG[ins.Rd] | (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.xor:
                    REG[ins.Rd] = REG[ins.Rd] ^ (REG[ins.Rs] + ins.Imm);
                    break;
                case Operation.not:
                    REG[ins.Rd] = ~(REG[ins.Rd] & (REG[ins.Rs] + ins.Imm));
                    break;
                case Operation.shr:
                    REG[ins.Rd] = REG[ins.Rd] >> (int)((REG[ins.Rs] + ins.Imm) & 0x001F);
                    break;
                case Operation.shl:
                    REG[ins.Rd] = REG[ins.Rd] << (int)((REG[ins.Rs] + ins.Imm) & 0x001F);
                    break;
                case Operation.ldb:
                    REG[ins.Rd] = ldb(ins.Rs + ins.Imm);
                    break;
                case Operation.lds:
                    REG[ins.Rd] = lds(ins.Rs + ins.Imm);
                    break;
                case Operation.ldw:
                    REG[ins.Rd] = ldw(ins.Rs + ins.Imm);
                    break;
                case Operation.sdb:
                    sdb(ins.Rd, ins.Rs + ins.Imm);
                    break;
                case Operation.sds:
                    sds(ins.Rd, ins.Rs + ins.Imm);
                    break;
                case Operation.sdw:
                    sdw(ins.Rd, ins.Rs + ins.Imm);
                    break;
                case Operation.sifl:

                    break;
                default:
                    break;
            }
            if (ins.IsImm) PC += 4;
            else PC += 2;
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
