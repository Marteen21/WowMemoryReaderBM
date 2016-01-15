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
        private static DoT haunt = new DoT(48181,Const.WindowsVirtualKey.K_6);
        private static DoT baneofAgony = new DoT(980, Const.WindowsVirtualKey.K_8);
        private static Spell shadowTrance = new Spell(17941, Const.WindowsVirtualKey.K_9);
        private static DoT drainLife = new DoT(689, Const.WindowsVirtualKey.K_Ö);
        private static DoT drainSoul = new DoT(1120, Const.WindowsVirtualKey.K_Ü);
        private static Spell fellFlame = new Spell(77799, Const.WindowsVirtualKey.K_Ő);
        private static Curse curseoftheElements = new Curse(1490, Const.WindowsVirtualKey.K_Á);
        private static Spell lifeTap = new Spell(1454, Const.WindowsVirtualKey.K_T);

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

        internal static DoT Haunt
        {
            get
            {
                return haunt;
            }

            set
            {
                haunt = value;
            }
        }

        public static Spell ShadowTrance
        {
            get
            {
                return shadowTrance;
            }

            set
            {
                shadowTrance = value;
            }
        }

        internal static DoT DrainLife
        {
            get
            {
                return drainLife;
            }

            set
            {
                drainLife = value;
            }
        }

        internal static DoT DrainSoul
        {
            get
            {
                return drainSoul;
            }

            set
            {
                drainSoul = value;
            }
        }

        public static Spell FellFlame
        {
            get
            {
                return fellFlame;
            }

            set
            {
                fellFlame = value;
            }
        }

        public static Curse CurseoftheElements
        {
            get
            {
                return curseoftheElements;
            }

            set
            {
                curseoftheElements = value;
            }
        }

        public static Spell LifeTap
        {
            get
            {
                return lifeTap;
            }

            set
            {
                lifeTap = value;
            }
        }
    }

}
