using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Models;
using WowMemoryReaderBM.Objects;

namespace WowMemoryReaderBM.Bots
{
    class DruidDPS
    {

        private static DoT rake = new DoT(1822, Const.WindowsVirtualKey.K_2);
        private static DoT rip = new DoT(1079, Const.WindowsVirtualKey.K_R);
        private static DoT mangle = new DoT(33876, Const.WindowsVirtualKey.K_5);
        private static DoT FF = new DoT(91565, Const.WindowsVirtualKey.K_4);
        private static DoT pounce = new DoT(9005, Const.WindowsVirtualKey.K_1);
        private static DoT shred = new DoT(58180, Const.WindowsVirtualKey.K_1);
        private static Spell catform = new Spell(768);
        private static Spell prowl = new Spell(5215);

        public static void DpsEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            UInt64 CurrTargetGUID = Program.wow.ReadUInt64((uint)Program.wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.CurrentTargetGUID);
            Program.PlayerObject.Refresh();
            if (CurrTargetGUID != 0)
            {
                Program.TargetObject = new GameObject(CurrTargetGUID);
                Program.TargetObject.RefreshBuffIDs();


                if (Program.PlayerObject.HasBuff(catform.ID))
                {
                    if (!Program.PlayerObject.HasBuff(prowl.ID))
                    {
                        DruidDPS.rake.ReCast(Program.TargetObject);
                        DruidDPS.mangle.ReCast(Program.TargetObject);
                        DruidDPS.FF.ReCast(Program.TargetObject);
                        // if ((uint)Const.Globals.ComboPoint <= 4)
                        // {
                        DruidDPS.rip.ReCast(Program.TargetObject);
                        // }
                        if(Program.PlayerObject.Manapercent >= 10)
                        {
                            DruidDPS.shred.SendCast();
                        }
                    }
                    else
                    {
                        DruidDPS.pounce.SendCast();
                    }

                }

            }

        }

    }
}


//    FFBuff="Faerie Fire(Feral)";
//    FFKey=4;

//    chargeKey=0;



//    // range-ben van e a target 
//    // a hátát látom e a targetnak 
//    if(targetEnemy==1){
//        if(FFBuff=0 && range<30)
//        {
//            SendKey(FFkey);
//        }

//        if (range>8 && range<25)
//        {
//            chargeKey
//        }

//        if(range<melee)
//        {
//            if(mangleBuff==0)
//            {
//                mangleKey
//            }
//            if(rakeBuff==0)
//            {
//                rakeKey
//            }
//            if(combo=5 && ripBuff==0)
//            {
//                ripKey
//            }
//            if()           

//        }   


//}

//if (Program.PlayerObject.Healthpercent >= 80 && Program.PlayerObject.Manapercent <= 50) {
//    WarlockDPS.LifeTap.SendCast();
//}
//if (CurrTargetGUID != 0) {
//    Program.TargetObject = new GameObject(CurrTargetGUID);
//    Program.TargetObject.RefreshBuffIDs();
//    if (!WarlockDPS.Corruption.ReCast(Program.TargetObject) && !WarlockDPS.BaneofAgony.ReCast(Program.TargetObject) && !WarlockDPS.ShadowTrance.IfCast(Program.PlayerObject)) {
//        if (!Program.PlayerObject.IsMoving) {
//            if (!WarlockDPS.UnstableAffliction.ReCast(Program.TargetObject) && !WarlockDPS.Haunt.ReCast(Program.TargetObject)) {
//                WarlockDPS.DrainLife.ReCast(Program.TargetObject);
//            }
//        }
//        else {
//            if (!WarlockDPS.CurseoftheElements.ReCast(Program.TargetObject)) {
//                WarlockDPS.FellFlame.SendCast();
//            }
//        }

//    }
//}




