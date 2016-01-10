using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowMemoryReaderBM.Constants {
    class Masker {
        internal enum offset : uint {
            UnitType = 52,
            UnitID = 32,

        }
        internal enum mask : uint {
            UnitType = 0x007,
            UnitID = 0x0000FFFF,

        }
        public static UInt64 Guid2UnitType(UInt64 b) {
            return ((b >> (int)offset.UnitType)&(uint)mask.UnitType);
        }
        public static UInt64 Guid2UnitID(UInt64 b) {
            return ((b >> (int)offset.UnitID)& (uint)mask.UnitID);
        }
    }
}
