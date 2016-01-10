using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowMemoryReaderBM.Constants {
    class Offsets {
        internal enum ObjectManager : uint {
            CurMgrPointer = 0x9BE7E0,
            CurMgrOffset = 0x463C,
            NextObject = 0x3C,
            FirstObject = 0xC0,
            LocalGUID = 0xC8
        }
        internal enum descriptors : uint {
            Level = 0xB0,
            Health = 0x58,
            MaxHealth = 0x70,
            Mana = 0x5C,
            MaxMana = 0x74,
            TargetGuid64 = 0x40,

        }
    }
}
