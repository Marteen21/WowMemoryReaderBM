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
        public static ObjectStorage PlayerObject;
        private static uint ObjMgr;
        

        static void Main(string[] args) {
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            Console.WriteLine("0x{0:X}", (uint)wow.MainModule.BaseAddress);
            //PlayerObject = new ObjectStorage(wow.MainModule.BaseAddress, new uint[] { 0x00A42788, 0x9C, 0x5C });
            UInt64 CurrTargetGUID = wow.ReadUInt64((uint)wow.MainModule.BaseAddress + (uint)Offsets.Globals.CurrentTargetGUID);
            //ObjectStorage Player2Object = new ObjectStorage(wow.MainModule.BaseAddress, new uint[] { 0x00a70c50, 0x38, 0x24 });
            ObjMgr = wow.ReadUInt(wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Offsets.ObjectManager.CurMgrPointer)+ (uint)Offsets.ObjectManager.CurMgrOffset);
            Console.WriteLine(ObjMgr);
            FirstObject = new GameObject(wow.ReadUInt(ObjMgr+(uint)Offsets.ObjectManager.FirstObject));
            Console.WriteLine(FirstObject.BaseAddress);
            Console.WriteLine("0x{0:X}",FirstObject.Guid);

            //Console.WriteLine(Masker.Guid2UnitID(PlayerObject.Targetguid));
            ObjectStorage TargetObject = new ObjectStorage();
            TargetObject.Guid = CurrTargetGUID;
            TargetObject.SetObjectBaseByGuid();
            ObjectStorage Bela = new ObjectStorage();
            Bela.BaseAddress = wow.ReadUInt(TargetObject.BaseAddress + 0xC)+0x10;
            Bela.PrintDescriptorsToConsole();
            //TargetObject.PrintDescriptorsToConsole();
            //Offsets.PrintGlobalstoConsole();
            Console.ReadLine();
        }
    }
}
