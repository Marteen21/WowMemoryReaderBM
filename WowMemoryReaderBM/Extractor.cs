using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Objects;
using WowMemoryReaderBM.Constants;
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
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing START" + "//////".PadLeft(paddistance / 2));
            try {
                Console.WriteLine("BaseAddress: ".PadRight(paddistance) + String.Format("0x{0:X}", go.BaseAddress));
                Console.WriteLine("StorageAddress: ".PadRight(paddistance) + String.Format("0x{0:X}", go.ObjectStorageAddress));
                Console.WriteLine("GUID: ".PadRight(paddistance) + String.Format("0x{0:X}", go.Guid));


            }
            catch {
                Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing ERROR" + "//////".PadLeft(paddistance / 2));
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Storage Printing END" + "//////".PadLeft(paddistance / 2));
        }
        public static void PrintBuffData(GameObject go) {
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Buff Printing START" + "//////".PadLeft(paddistance / 2));
            foreach (Offsets.buffs enumValue in Enum.GetValues(typeof(Offsets.buffs))) {
                try {
                    if (enumValue.ToString().Contains("64")) {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("0x{0:X}", Program.wow.ReadUInt64(go.BaseAddress + (uint)enumValue)));
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(paddistance) + String.Format("{0}", Program.wow.ReadUInt(go.BaseAddress + (uint)enumValue)));
                    }

                }
                catch {
                    Console.WriteLine("//////".PadRight(paddistance / 2) + "Buff Printing ERROR" + "//////".PadLeft(paddistance / 2));
                }
            }
            Console.WriteLine("//////".PadRight(paddistance / 2) + "Buff Printing END" + "//////".PadLeft(paddistance / 2));
        }

    }
}

