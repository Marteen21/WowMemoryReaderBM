using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Models;
using WowMemoryReaderBM.Objects;

namespace WowMemoryReaderBM.Bots {
    class DruidDPS {

        public static DoT rake = new DoT(1822, Const.WindowsVirtualKey.K_2);
        public static DoT rip = new DoT(1079, Const.WindowsVirtualKey.K_R);
        public static DoT mangle = new DoT(33917, Const.WindowsVirtualKey.K_5);
        public static DoT FF = new DoT(770, Const.WindowsVirtualKey.K_4);



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
        public static void DpsEvent(Object source, System.Timers.ElapsedEventArgs e) {
            UInt64 CurrTargetGUID = Program.wow.ReadUInt64((uint)Program.wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.CurrentTargetGUID);
            Program.PlayerObject.Refresh();
            if (Program.PlayerObject.Healthpercent >= 80 && Program.PlayerObject.Manapercent <= 50) {
                WarlockDPS.LifeTap.SendCast();
            }
            if (CurrTargetGUID != 0) {
                Program.TargetObject = new GameObject(CurrTargetGUID);
                Program.TargetObject.RefreshBuffIDs();
                if (!WarlockDPS.Corruption.ReCast(Program.TargetObject) && !WarlockDPS.BaneofAgony.ReCast(Program.TargetObject) && !WarlockDPS.ShadowTrance.IfCast(Program.PlayerObject)) {
                    if (!Program.PlayerObject.IsMoving) {
                        if (!WarlockDPS.UnstableAffliction.ReCast(Program.TargetObject) && !WarlockDPS.Haunt.ReCast(Program.TargetObject)) {
                            WarlockDPS.DrainLife.ReCast(Program.TargetObject);
                        }
                    }
                    else {
                        if (!WarlockDPS.CurseoftheElements.ReCast(Program.TargetObject)) {
                            WarlockDPS.FellFlame.SendCast();
                        }
                    }

                }
            }
        }
    }
}


