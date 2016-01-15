using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;

namespace WowMemoryReaderBM.Models {
    class DoT : Spell {
        public DoT(uint i) : base(i) {

        }
        public DoT(uint i, Const.WindowsVirtualKey kb):base(i, kb) {

        }
    }
}
