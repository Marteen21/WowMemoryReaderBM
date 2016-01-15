using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Objects;
using WowMemoryReaderBM.Constants;
//using WowMemoryReaderBM.Database;
namespace WowMemoryReaderBM {
    class Extractor {
        private static int paddistance = 30;
        public static void PrintStorageDescriptors(GameObject go) {
            Console.WriteLine("//////".PadRight(paddistance/2) + "Storage Printing START" +"//////".PadLeft(paddistance/2));
            try {
                Console.WriteLine("ObjectStorage: ".PadRight(paddistance) + String.Format("0x{0:X}", go.ObjectStorageAddress) );
            }
            catch {
                Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing ERROR" + "//////".PadLeft(paddistance / 2));
            }
            foreach (Offsets.descriptors enumValue in Enum.GetValues(typeof(Offsets.descriptors))) {
                try {
                    if (enumValue.ToString().Contains("64")) {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("0x{0:X}", Program.wow.ReadUInt64(go.ObjectStorageAddress + (uint)enumValue)));
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("{0}", Program.wow.ReadUInt(go.ObjectStorageAddress + (uint)enumValue)));
                    }

                }
                catch {
                    Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing ERROR" + "//////".PadLeft(paddistance / 2));
                }
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing END" + "//////".PadLeft(paddistance / 2));
        }
        public static void PrintGlobals() {
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Global Printing START" + "//////".PadLeft(paddistance / 2));
            foreach (Offsets.Globals enumValue in Enum.GetValues(typeof(Offsets.Globals))) {
                try {
                    if (enumValue.ToString().Contains("GUID")) {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("0x{0:X}", Program.wow.ReadUInt64((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue)).PadRight(paddistance) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }
                    else if (enumValue.ToString().Contains("Name") || enumValue.ToString().Contains("Text")) {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + Program.wow.ReadASCIIString(((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue), 64).PadRight(paddistance) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + (Program.wow.ReadUInt((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue)).ToString().PadRight(paddistance) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }

                }
                catch {
                    Console.WriteLine("//////".PadRight(paddistance / 2) + "Global Printing ERROR" + "//////".PadLeft(paddistance / 2));
                }
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Global Printing END" + "//////".PadLeft(paddistance / 2));
        }
        public static void PrintStorageGear(GameObject go) {
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Gear Printing START" + "//////".PadLeft(paddistance / 2));
            try {
                Console.WriteLine("ObjectStorage: ".PadRight(paddistance) + String.Format("0x{0:X}", go.ObjectStorageAddress));
            }
            catch {
                Console.WriteLine("//////".PadRight(paddistance / 2) + "Gear Printing ERROR" + "//////".PadLeft(paddistance / 2));
            }
            foreach (Offsets.gear enumValue in Enum.GetValues(typeof(Offsets.gear))) {
                try {
                    if (enumValue.ToString().Contains("64")) {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("0x{0:X}", Program.wow.ReadUInt64(go.ObjectStorageAddress + (uint)enumValue)));
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("{0}", Program.wow.ReadUInt(go.ObjectStorageAddress + (uint)enumValue)));
                    }

                }
                catch {
                    Console.WriteLine("//////".PadRight(paddistance / 2) + "Gear Printing ERROR" + "//////".PadLeft(paddistance / 2));
                }
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Gear Printing END" + "//////".PadLeft(paddistance / 2));
        }
        public static void PrintGameObjectData(GameObject go) {
            Console.WriteLine("//////".PadRight(paddistance / 2) + "OBJECT Printing START" + "//////".PadLeft(paddistance / 2));
            try {
                Console.WriteLine("BaseAddress: ".PadRight(paddistance) + String.Format("0x{0:X}", go.BaseAddress));
                Console.WriteLine("StorageAddress: ".PadRight(paddistance) + String.Format("0x{0:X}", go.ObjectStorageAddress));
                Console.WriteLine("BuffAddress: ".PadRight(paddistance) + String.Format("0x{0:X}", go.BuffAddress));
                Console.WriteLine("GUID: ".PadRight(paddistance) + String.Format("0x{0:X}", go.Guid));
                

            }
            catch {
                Console.WriteLine("//////".PadRight(paddistance / 2) + "OBJECT Printing ERROR" + "//////".PadLeft(paddistance / 2));
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "OBJECT Printing END" + "//////".PadLeft(paddistance / 2));
        }
        public static void PrintBuffs(GameObject go) {
            Console.WriteLine("TOP KEK");
            foreach (uint s in go.BuffIDs) {
                Console.WriteLine(s + "  " + s);
            }

        }

        public static void PrintPointers(GameObject go) {
            Console.Clear();
            Console.WriteLine("0x{0:X}",go.BaseAddress);
            uint goal = 0x27050128;

            uint temp = 0;
            for (int i = 0; i < 10000; i++) {
                temp = Program.wow.ReadUInt(go.BaseAddress + (uint)(i));
                if (temp > goal - 0x1000 && temp < goal) {
                    Console.WriteLine(String.Format("0x{0:X}", i) + "    " + String.Format("0x{0:X}", temp));
                }
            
                //if (temp != 0) {
                //    Console.WriteLine(String.Format("0x{0:X}", i) + "    " + String.Format("0x{0:X}", temp));
                //}
            }
        }
        public static void PrintBuffPointers() {
            Console.Clear();
            uint baseaddr1 =0x0bba8220;
            uint baseaddr2 =0x0bfcbd00;

            uint goal2 = 0x0bfcb6a0;
            uint goal1 = 0x0bba6ea0;
            Console.WriteLine((goal2 - baseaddr2) + " " + (goal1 - baseaddr1));
            uint temp1 = 0;
            uint temp2 = 0;
            for (int i = 0; i < 10000; i++) {
                temp1 = Program.wow.ReadUInt(baseaddr1 + (uint)(i));
                temp2 = Program.wow.ReadUInt(baseaddr2 + (uint)(i));
                if ((temp1 > goal1 - 0x1000 && temp1 < goal1) || (temp2 > goal2 - 0x1000 && temp2 < goal2)) {
                    Console.WriteLine(String.Format("0x{0:X}", i).PadRight(30) + String.Format("0x{0:X}", (goal1-temp1)-(goal2- temp2)).PadRight(30) );
                }
            }
        }
        public static void TopKek(GameObject go1, GameObject go2) {
            uint temp1, temp2;
            for (int i = 0; i < 10000; i++) {
                temp1 = Program.wow.ReadUInt(go1.BaseAddress + (uint)(i));
                temp2 = Program.wow.ReadUInt(go2.BaseAddress + (uint)(i));
                if (go1.BuffAddress-temp1==go2.BuffAddress-temp2) {
                    Console.WriteLine(String.Format("Pointer: 0x{0:X}", i).PadRight(30)+String.Format("Offset: 0x{0:X}",go1.BuffAddress- temp1));
                }
            }
        }
        public static void TopKek2(GameObject go1, GameObject go2) {
            uint temp1, temp2;
            for (int i = 0; i < 10000; i++) {
                temp1 = Program.wow.ReadUInt(go1.ObjectStorageAddress + (uint)(i));
                temp2 = Program.wow.ReadUInt(go2.ObjectStorageAddress + (uint)(i));
                if (go1.BuffAddress - temp1 == go2.BuffAddress - temp2) {
                    Console.WriteLine(String.Format("Pointer: 0x{0:X}", i).PadRight(30) + String.Format("Offset: 0x{0:X}", go1.BuffAddress - temp1));
                }
            }
        }
        public static void PrintPointersStorage(GameObject go) {
            uint temp = 0;
            Console.WriteLine("Storage");
            for (int i = 0; i < 10000; i++) {
                temp = Program.wow.ReadUInt(go.ObjectStorageAddress + (uint)(i));
                if (temp > 0x1c000000 && temp < 0x1d000000) {
                    Console.WriteLine(String.Format("0x{0:X}", i) + "    " + String.Format("0x{0:X}", temp));
                }
            }
        }
    }

}

