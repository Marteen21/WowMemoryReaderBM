using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;

namespace WowMemoryReaderBM.Objects {
    class GameObject {
        private uint baseAddress;
        private UInt64 guid;
        private uint objectStorageAddress;

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

        public GameObject() {
            this.BaseAddress = 0;
            this.Guid = 0;
            this.ObjectStorageAddress = 0;
        }

        public GameObject(uint baddr) { //Constructor from base Address
            this.BaseAddress = baddr;
            this.Guid = Program.wow.ReadUInt64(baddr + (uint)Offsets.ObjectManager.LocalGUID);
            this.ObjectStorageAddress = Program.wow.ReadUInt(this.BaseAddress + 0xC) + 0x10;

        }
        public GameObject(UInt64 gid) { //Constructor from GUID
            this.Guid = gid;
            if (gid == 0) {     //If GUID zero, return blank Object
                this.BaseAddress = 0;
                this.ObjectStorageAddress = 0;
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
                    return;
                }
            }
        }

    }
}
