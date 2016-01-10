using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;

namespace WowMemoryReaderBM.Objects {
    class GameObject {
        uint baseAddress;
        UInt64 guid;
        UIntPtr objectStorage;
        UIntPtr nextObject;

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

        public UIntPtr NextObject {
            get {
                return nextObject;
            }

            set {
                nextObject = value;
            }
        }

        public UIntPtr ObjectStorage {
            get {
                return objectStorage;
            }

            set {
                objectStorage = value;
            }
        }

        public GameObject(uint baddr) {
            BaseAddress = baddr;
            Guid = Program.wow.ReadUInt64(baddr + (uint)Offsets.ObjectManager.LocalGUID);
        }

    }
}
