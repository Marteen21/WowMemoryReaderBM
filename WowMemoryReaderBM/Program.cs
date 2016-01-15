using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magic;
using WowMemoryReaderBM.Objects;
using WowMemoryReaderBM.Constants;
//using WowMemoryReaderBM.Database;
using WowMemoryReaderBM.Bots;
using WowMemoryReaderBM.Models;

namespace WowMemoryReaderBM {
    class Program {
        const string PROCESS_WINDOW_TITLE = "World of Warcraft";
        public static BlackMagic wow;
        public static GameObject FirstObject;
        public static GameObject TargetObject;
        public static GameObject PlayerObject;
        private static uint ObjMgrAddr;

        static void Main(string[] args) {
            //Open the proccess
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            //Setup Object Manager and First object base address
            ObjMgrAddr = wow.ReadUInt(wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Constants.Const.ObjectManager.CurMgrPointer) + (uint)Constants.Const.ObjectManager.CurMgrOffset);
            FirstObject = new GameObject(wow.ReadUInt(ObjMgrAddr + (uint)Constants.Const.ObjectManager.FirstObject));
            //Read TargetGUID from globals and find in the Object Manager
            //UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Const.Globals.CurrentTargetGUID);
            UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.CurrentTargetGUID);
            PlayerObject = new GameObject(wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.PlayerGUID));
            TargetObject = new GameObject(CurrTargetGUID);
            PlayerObject.Wowclass = wow.ReadByte(PlayerObject.DescriptorArrayAddress + (uint)Const.descriptors.Class8);

            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Interval = 100;
            if (PlayerObject.Wowclass == 9) {
                Console.WriteLine("Initiate Affliction Warlock DPS BOT v1.0");
                aTimer.Elapsed += WarlockDPS.DpsEvent;
            }
            else if (PlayerObject.Wowclass == 11) {
                Console.WriteLine("Initiate Feral Druid DPS BOT v0.1");
                aTimer.Elapsed += DruidDPS.DpsEvent;
            }
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
            while (true) {
                switch (Console.ReadLine()) {
                    case "stop":
                        Console.WriteLine("STOP");
                        aTimer.Elapsed -= DruidDPS.DpsEvent;
                        aTimer.Elapsed -= WarlockDPS.DpsEvent;
                        aTimer.Elapsed -= PrinterEvent;
                        break;
                    case "printer":
                        aTimer.Elapsed += PrinterEvent;
                        break;
                    case "start":
                        if (PlayerObject.Wowclass == 9) {
                            Console.WriteLine("Initiate Affliction Warlock DPS BOT v1.0");
                            aTimer.Elapsed += WarlockDPS.DpsEvent;
                        }
                        else if (PlayerObject.Wowclass == 11) {
                            Console.WriteLine("Initiate Feral Druid DPS BOT v0.1");
                            aTimer.Elapsed += DruidDPS.DpsEvent;
                        }
                        break;
                }
            }
        }
        #region TimerInterrupts
        //private static void DpsEvent(Object source, System.Timers.ElapsedEventArgs e) {
        //    UInt64 CurrTargetGUID = Program.wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.CurrentTargetGUID);
        //    PlayerObject.Refresh();
        //    if(PlayerObject.Healthpercent>=80 && PlayerObject.Manapercent <= 50) {
        //        WarlockDPS.LifeTap.SendCast();
        //    }
        //    if (CurrTargetGUID != 0) {
        //        TargetObject = new GameObject(CurrTargetGUID);
        //        TargetObject.RefreshBuffIDs();
        //        if (!WarlockDPS.Corruption.ReCast(TargetObject) && !WarlockDPS.BaneofAgony.ReCast(TargetObject) && !WarlockDPS.ShadowTrance.IfCast(PlayerObject)) {
        //            if (!PlayerObject.IsMoving) {
        //                if (!WarlockDPS.UnstableAffliction.ReCast(TargetObject) && !WarlockDPS.Haunt.ReCast(TargetObject)) {
        //                    WarlockDPS.DrainLife.ReCast(TargetObject);
        //                }
        //            }
        //            else {
        //                if (!WarlockDPS.CurseoftheElements.ReCast(TargetObject)) {
        //                    WarlockDPS.FellFlame.SendCast();
        //                }
        //            }

        //        }
        //        }



        //}
        private static void PrinterEvent(Object source, System.Timers.ElapsedEventArgs e) {
            UInt64 CurrTargetGUID = Program.wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.CurrentTargetGUID);
            if (CurrTargetGUID != 0) {
                TargetObject = new GameObject(CurrTargetGUID);
                TargetObject.RefreshBuffIDs();
                Extractor.PrintBuffs(TargetObject);
                if (TargetObject.IsMoving) {
                    Console.WriteLine("MOVING");
                }
            }
            else {
                Console.WriteLine("No target");
            }
        }
        #endregion

    }
}
