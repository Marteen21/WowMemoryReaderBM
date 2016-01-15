using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magic;
using WowMemoryReaderBM.Objects;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Database;
using WowMemoryReaderBM.Bots;
namespace WowMemoryReaderBM {
    class Program {
        const string PROCESS_WINDOW_TITLE = "World of Warcraft";
        public static BlackMagic wow;
        public static GameObject FirstObject;
        public static GameObject TargetObject;
        //public static wowDBEntities db;
        private static uint ObjMgrAddr;
        

        static void Main(string[] args) {
            //Open the proccess
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            //db = new wowDBEntities();
            //Setup Object Manager and First object base address
            ObjMgrAddr = wow.ReadUInt(wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Offsets.ObjectManager.CurMgrPointer)+ (uint)Offsets.ObjectManager.CurMgrOffset);
            FirstObject = new GameObject(wow.ReadUInt(ObjMgrAddr+(uint)Offsets.ObjectManager.FirstObject));
            //Read TargetGUID from globals and find in the Object Manager
            //UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Offsets.Globals.CurrentTargetGUID);
            //TargetObject = new GameObject(CurrTargetGUID);
            //Printing data
            //Extractor.PrintStorageDescriptors(TargetObject);
            //Extractor.PrintGlobals();
            //Extractor.PrintStorageGear(TargetObject);
            Extractor.PrintGameObjectData(TargetObject);

            //System.Timers.Timer aTimer = new System.Timers.Timer();
            //aTimer.Interval = 500;
            //aTimer.Elapsed += OnTimedEvent;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;


            while (true) {
                UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Offsets.Globals.CurrentTargetGUID);
                TargetObject = new GameObject(CurrTargetGUID);
                Extractor.PrintGameObjectData(TargetObject);
                if (TargetObject.BuffAddress > 0x1000) {
                    Console.WriteLine("BELA");
                    TargetObject.RefreshBuffIDs();
                    Extractor.PrintBuffs(TargetObject);
                }
                else {
                    Console.WriteLine("COULDNT FIND BUFFS IN BELA :(");
                    TargetObject.RefreshMystBuffIDs();
                    Extractor.PrintBuffs(TargetObject);
                }
                Console.ReadLine();
            }
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e) {
            TargetObject.RefreshBuffIDs();
            uint SpellPending = wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Offsets.Globals.SpellIsPending);
            if ((!TargetObject.BuffIDs.Exists(x => x == 5782)) && SpellPending==0) {
                Console.WriteLine("Casting Fear");
                SendKeys.Send(VirtualKeys.WK_KEY_F);
            }
        }
        private static void OnTimedEvent2(Object source, System.Timers.ElapsedEventArgs e) {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            Extractor.PrintGlobals();

        }

    }
}
