using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Models;

namespace WowMemoryReaderBM.Bots {
    public class WarlockDPS {
        private static DoT corruption = new DoT(172,Const.WindowsVirtualKey.K_5);
        private static DoT unstableAffliction = new DoT(30108, Const.WindowsVirtualKey.K_7);
        private static DoT haunt = new DoT(48181);
        private static DoT baneofAgony = new DoT(980, Const.WindowsVirtualKey.K_8);
        private static Curse curseoftheElements = new Curse(1490);

        internal static DoT Corruption
        {
            get
            {
                return corruption;
            }

            set
            {
                corruption = value;
            }
        }

        internal static DoT UnstableAffliction
        {
            get
            {
                return unstableAffliction;
            }

            set
            {
                unstableAffliction = value;
            }
        }

        internal static DoT BaneofAgony
        {
            get
            {
                return baneofAgony;
            }

            set
            {
                baneofAgony = value;
            }
        }
    }

}
