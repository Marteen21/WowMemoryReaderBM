using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magic;
using WowMemoryReaderBM.Objects;
using WowMemoryReaderBM.Constants;
namespace WowMemoryReaderBM {
    class Program {
        const string PROCESS_WINDOW_TITLE = "World of Warcraft";
        public static BlackMagic wow;
        public static GameObject FirstObject;
        private static uint ObjMgrAddr;
        

        static void Main(string[] args) {
            //Open the proccess
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            //Setup Object Manager and First object base address
            ObjMgrAddr = wow.ReadUInt(wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Offsets.ObjectManager.CurMgrPointer)+ (uint)Offsets.ObjectManager.CurMgrOffset);
            FirstObject = new GameObject(wow.ReadUInt(ObjMgrAddr+(uint)Offsets.ObjectManager.FirstObject));
            //Read TargetGUID from globals and find in the Object Manager
            UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Offsets.Globals.CurrentTargetGUID);
            GameObject TargetObject = new GameObject(CurrTargetGUID);
            //Printing data
            Extractor.PrintStorageDescriptors(TargetObject);
            Extractor.PrintGlobals();
            Extractor.PrintStorageGear(TargetObject);
            Extractor.PrintGameObjectData(TargetObject);
            Extractor.PrintBuffData(TargetObject);


            Console.ReadLine();
        }
    }
}
