using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Database;

namespace WowMemoryReaderBM.Objects {
    class GameObject {
        private uint baseAddress;
        private UInt64 guid;
        private uint objectStorageAddress;
        private uint buffAddress;
        //private List<spells> buffs;
        private List<uint> buffIDs;

        public uint BaseAddress {
            get {
                return baseAddress;
            }

            set {
                baseAddress = value;
            }
        }

        public ulong Guid {
            get {
                return guid;
            }

            set {
                guid = value;
            }
        }


        public uint ObjectStorageAddress {
            get {
                return objectStorageAddress;
            }

            set {
                objectStorageAddress = value;
            }
        }

        public uint BuffAddress {
            get {
                return buffAddress;
            }

            set {
                buffAddress = value;
            }
        }

        public List<uint> BuffIDs
        {
            get
            {
                return buffIDs;
            }

            set
            {
                buffIDs = value;
            }
        }

        //public List<spells> Buffs
        //{
        //    get
        //    {
        //        return buffs;
        //    }

        //    set
        //    {
        //        buffs = value;
        //    }
        //}

        public GameObject() {
            this.BaseAddress = 0;
            this.Guid = 0;
            this.ObjectStorageAddress = 0;
        }

        public GameObject(uint baddr) { //Constructor from base Address
            this.BaseAddress = baddr;
            this.Guid = Program.wow.ReadUInt64(baddr + (uint)Offsets.ObjectManager.LocalGUID);
            this.ObjectStorageAddress = Program.wow.ReadUInt(this.BaseAddress + 0xC) + 0x10;
            this.BuffAddress = Program.wow.ReadUInt(this.baseAddress + 0xe9c) + 0x4;
            this.BuffIDs = new List<uint>();
            //this.BuffAddress = this.baseAddress + 0xe98;
            //this.Buffs = new List<spells>();
        }
        public GameObject(UInt64 gid) { //Constructor from GUID
            this.Guid = gid;
            if (gid == 0) {     //If GUID zero, return blank Object
                this.BaseAddress = 0;
                this.ObjectStorageAddress = 0;
                this.BuffAddress = 0;
                return;
            }
            else {  //Iterate through the objects (linked list) from first object till the GUID matches
                GameObject TempObject = new GameObject();
                TempObject.BaseAddress = Program.FirstObject.BaseAddress;
                try {
                    while (TempObject.BaseAddress != 0) {
                        TempObject.Guid = Program.wow.ReadUInt64(TempObject.BaseAddress + 0x30);
                        if (TempObject.Guid == this.guid) {
                            this.BaseAddress = TempObject.BaseAddress;
                            this.ObjectStorageAddress = Program.wow.ReadUInt(this.BaseAddress + 0xC) + 0x10;
                            this.BuffAddress = Program.wow.ReadUInt(this.baseAddress + 0xe9c) + 0x4;
                            //this.BuffAddress = this.baseAddress + 0xe98;
                            //this.Buffs = new List<spells>();
                            this.BuffIDs = new List<uint>();
                            return;
                        }
                        else {
                            TempObject.BaseAddress = Program.wow.ReadUInt(TempObject.BaseAddress + (uint)Offsets.ObjectManager.NextObject);
                        }
                    }
                }
                catch {
                    this.BaseAddress = 0;
                    this.ObjectStorageAddress = 0;
                    this.buffAddress = 0;
                    this.BuffIDs = new List<uint>();
                    //this.Buffs = new List<spells>();
                    return;
                }
            }
        }
        //public void RefreshBuffs() {
        //    this.Buffs.Clear();
        //    int temp = 1;
        //    uint i = 0;
        //    while (temp != 0) {
        //        temp = Program.wow.ReadInt(this.BuffAddress + (0x08 * i));
        //        i++;
        //        if (temp != 0) {
        //            this.Buffs.Add(Program.db.spells.Find(temp));
        //        }
        //    }           
        //}
        public void RefreshBuffIDs() {
            uint temp = 1;
            uint i = 0;
            this.BuffIDs.Clear();
            while(temp != 0) {
                temp = Program.wow.ReadUInt(this.BuffAddress + (0x08 * i));
                i++;
                if (temp != 0) {
                    //this.Buffs.Add(Program.db.spells.Find(temp));
                    this.BuffIDs.Add(temp);
                }
            }
        }

    }
}
