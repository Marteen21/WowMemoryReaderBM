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

        static void Main(string[] args) {
            wow = new BlackMagic();
            wow.OpenProcessAndThread(SProcess.GetProcessFromWindowTitle(PROCESS_WINDOW_TITLE));
            PlayerObject = new GameObject(wow.MainModule.BaseAddress, new uint[] { 0x00A42788, 0x9C, 0x5C });
            FirstObject = new GameObject(wow.MainModule.BaseAddress, new uint[] { (uint)Offsets.ObjectManager.CurMgrPointer, (uint)Offsets.ObjectManager.CurMgrOffset, (uint)Offsets.ObjectManager.FirstObject });
            FirstObject.BaseAddress += 0x10;
            PlayerObject.PrintDescriptorsToConsol();
            FirstObject.PrintDescriptorsToConsol();
            Console.WriteLine(FirstObject.BaseAddress);
            Console.WriteLine("0x{0:X}",PlayerObject.Targetguid);
            Console.WriteLine("0x{0:X}", Masker.Guid2UnitType(PlayerObject.Targetguid));
            Console.WriteLine(Masker.Guid2UnitID(PlayerObject.Targetguid));
            GameObject TargetObject = new GameObject();
            TargetObject.Guid = PlayerObject.Targetguid;
            TargetObject.SetObjectBaseByGuid();
            TargetObject.PrintDescriptorsToConsol();
            Console.ReadLine();
        }
        public static void mask(UInt64 a,UInt64 b) {
            Console.WriteLine(a & b);
        }
    }
}
