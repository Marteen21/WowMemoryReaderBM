using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;

namespace WowMemoryReaderBM.Models {
    public class Curse : Spell {
        public Curse(uint i) : base(i) {

        }
        public Curse(uint i, Const.WindowsVirtualKey kb) : base(i, kb) {

        }
    }
}
