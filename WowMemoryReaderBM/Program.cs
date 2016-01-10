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
        public static GameObject PlayerObject;
        private static uint ObjMgr;
        

        static void Main(string[] args) {
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            PlayerObject = new GameObject(wow.MainModule.BaseAddress, new uint[] { 0x00A42788, 0x9C, 0x5C });
            GameObject Player2Object = new GameObject(wow.MainModule.BaseAddress, new uint[] { 0x00a70c50, 0x38, 0x24 });
            ObjMgr = wow.ReadUInt(wow.ReadUInt((uint)wow.MainModule.BaseAddress + (uint)Offsets.ObjectManager.CurMgrPointer)+ (uint)Offsets.ObjectManager.CurMgrOffset);
            FirstObject = new GameObject();
            FirstObject.BaseAddress = wow.ReadUInt(ObjMgr + (uint)Offsets.ObjectManager.FirstObject);
            PlayerObject.PrintDescriptorsToConsol();
            Player2Object.PrintDescriptorsToConsol();
            FirstObject.PrintDescriptorsToConsol();

            Console.WriteLine(Masker.Guid2UnitID(PlayerObject.Targetguid));
            GameObject TargetObject = new GameObject();
            TargetObject.Guid = PlayerObject.Targetguid;
            TargetObject.SetObjectBaseByGuid();
            TargetObject.PrintDescriptorsToConsol();
            Offsets.PrintGlobalstoConsole();
            Console.ReadLine();
        }
        public static void mask(UInt64 a,UInt64 b) {
            Console.WriteLine(a & b);
        }
    }
}
