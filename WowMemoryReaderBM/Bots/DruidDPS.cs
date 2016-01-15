using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Models;



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
    }
}


