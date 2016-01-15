using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Objects;

namespace WowMemoryReaderBM.Models {
    public class Curse : Spell {
        private const uint cot = 1714;
        private const uint cote = 1490;
        private const uint cow = 702;
        private const uint coe = 18223;
        public Curse(uint i) : base(i) {

        }
        public Curse(uint i, Const.WindowsVirtualKey kb) : base(i, kb) {

        }
        public override bool ReCast(GameObject go) {
            if (!go.HasBuff(cot) && !go.HasBuff(cote) && !go.HasBuff(cow) && !go.HasBuff(coe)) {
                this.SendCast();
                return true;
            }
            else {
                return false;
            }
        }
    }
}
