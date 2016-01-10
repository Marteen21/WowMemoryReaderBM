using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magic;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM;
namespace WowMemoryReaderBM.Objects {
    class GameObject {
        private UInt32 guid;
        private UInt64 guid2;
        private uint baseAddress;
        private UInt64 targetguid;

        public uint BaseAddress {
            get {
                return baseAddress;
            }

            set {
                baseAddress = value;
            }
        }

        public uint Guid {
            get {
                return guid;
            }

            set {
                guid = value;
            }
        }

        public UInt64 Targetguid {
            get {
                return targetguid;
            }

            set {
                targetguid = value;
            }
        }

        public ulong Guid2 {
            get {
                return guid2;
            }

            set {
                guid2 = value;
            }
        }

        public GameObject() {
            this.guid = 0;
            this.Guid2 = 0;
            this.baseAddress = 0;
            this.targetguid = 0;
        }
        public GameObject(IntPtr gamebase, uint[] offsets) {
            this.guid = 0;
            this.targetguid = 0;
            ///
            uint tempaddress = (uint)gamebase;
            foreach (uint offset in offsets) {
                tempaddress = Program.wow.ReadUInt(tempaddress + offset);
            }
            this.baseAddress = (uint)tempaddress - 0x10;
            this.targetguid = Program.wow.ReadUInt64(baseAddress + (uint)Offsets.descriptors.TargetGuid64);
        }
        public void SetObjectBaseByGuid() {
            
            GameObject TempObject = new GameObject();
            TempObject.BaseAddress = Program.FirstObject.baseAddress;
            while (TempObject.BaseAddress != 0) {//Iterate through memory to find the matching guid
                TempObject.Guid = Program.wow.ReadUInt(TempObject.BaseAddress + (uint)Offsets.ObjectManager.LocalGUID);
                if (TempObject.Guid == this.guid) {
                    Console.WriteLine("FOUND IT");
                    this.BaseAddress = TempObject.BaseAddress+4556-0x58;
                    return;
                }
                else {
                    TempObject.BaseAddress = Program.wow.ReadUInt(TempObject.BaseAddress + (uint)Offsets.ObjectManager.NextObject);
                }
                Console.WriteLine(TempObject.BaseAddress.ToString().PadRight(20) + TempObject.Guid);
            }
        }
        public void PrintDescriptorsToConsol() {
            try {
                Console.WriteLine("BaseAddress".PadRight(20) + Program.wow.ReadUInt(this.baseAddress).ToString().PadRight(15) + "0x{0:X}",this.baseAddress);
                Console.WriteLine();
            }
            catch {

            }
            foreach (Offsets.descriptors enumValue in Enum.GetValues(typeof(Offsets.descriptors))) {
                try {
                    if (enumValue.ToString().Contains("64")) {
                        Console.WriteLine(enumValue.ToString().PadRight(20) + (Program.wow.ReadUInt64(this.baseAddress + (uint)enumValue)).ToString().PadRight(15) + "0x{0:X}", this.baseAddress + enumValue);
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(20) + (Program.wow.ReadUInt(this.baseAddress + (uint)enumValue)).ToString().PadRight(15) + "0x{0:X}", this.baseAddress + enumValue);
                    }
                
                }
                catch { }
            }
        }
    }
}
