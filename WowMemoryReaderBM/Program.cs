using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magic;
using WowMemoryReaderBM.Objects;
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
            FirstObject = new GameObject(wow.MainModule.BaseAddress, new uint[] { (uint)Constants.Offsets.ObjectManager.CurMgrPointer, (uint)Constants.Offsets.ObjectManager.CurMgrOffset, (uint)Constants.Offsets.ObjectManager.FirstObject });
            FirstObject.BaseAddress += 0x10;
            PlayerObject.PrintDescriptorsToConsol();
            FirstObject.PrintDescriptorsToConsol();
            Console.WriteLine(FirstObject.BaseAddress);

            GameObject TargetObject = new GameObject();
            TargetObject.Guid2 = PlayerObject.Targetguid;
            TargetObject.SetObjectBaseByGuid();
            TargetObject.PrintDescriptorsToConsol();
            Console.ReadLine();
        }
    }
}
