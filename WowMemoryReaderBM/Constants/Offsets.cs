using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowMemoryReaderBM.Constants {
    class Offsets {
        internal enum ObjectManager : uint {
            CurMgrPointer = 0x9BE7E0,
            CurMgrOffset = 0x463C,
            NextObject = 0x3C,
            FirstObject = 0xC0,
            LocalGUID = 0xC8
        }
        internal enum descriptors : uint {
            Level = 0xB0,
            Health = 0x58,
            MaxHealth = 0x70,
            Mana = 0x5C,
            MaxMana = 0x74,
            TargetGuid64 = 0x40,

        }
        internal enum Globals : uint {
            PlayerName = 0x9BE820,
            CurrentRealm = 0x9BE9AE,
            CurrentTargetGUID = 0xAD7448,
            LastTargetGUID = 0xAD7450,
            FocusTargetGUID = 0xAD7468,
            MouseOverGUID = 0xAD7438,
            PetGUID = 0xB43B60,
            FollowGUID = 0x9D61D8,
            ComboPoint = 0xAD74F1,
            LootWindow = 0xB45230,
            Timestamp = 0x9C0C7C,
            BuildNumber = 0xAB4214,
            GetMinimapZoneText = 0xAD7414,
            GetZoneText = 0xAD741C,
            GetSubZoneText = 0xAD7418,
            GetZoneID = 0xAD74B0,
            IsInGame = 0xAD7426,
            ContinentID = 0x8A2710,
            LastErrorMessage = 0xAD6828,
            IsLoadingOrConnecting = 0xABB9AC,
            GetCurrencyInfo = 0x914F48,
            GetHomeBindAreaId = 0x9D4D7C,
            PetSpellBookNumSpells = 0xB33CA4,
            PetSpellBookNumSpellsPtr = 0xB33CA8,
            SpellIsTargetting = 0xACD654,
            SpellIsPending = 0xACD770,
            ScriptGetLocale = 0x9732FC,
            CursorType = 0x93D250,
            MirrorTimer = 0xAD78D0,
            GetNumInstalledAddons = 0x93A74C,
            BaseAddons = 0x93A750,
            TotalGuildMembers = 0xB35ECC,
            GuildRosterInfoBase = 0xB35F64
        }
        public static void PrintGlobalstoConsole() {
            foreach (Offsets.Globals enumValue in Enum.GetValues(typeof(Offsets.Globals))) {
                try {
                    if (enumValue.ToString().Contains("GUID")) {
                        Console.WriteLine(enumValue.ToString().PadRight(30) + String.Format("0x{0:X}", Program.wow.ReadUInt64((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue)).PadRight(30) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }
                    else if(enumValue.ToString().Contains("Name") || enumValue.ToString().Contains("Text")){
                        Console.WriteLine(enumValue.ToString().PadRight(30) + Program.wow.ReadASCIIString(((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue),64).PadRight(30) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }
                    else {
                        Console.WriteLine(enumValue.ToString().PadRight(30) + (Program.wow.ReadUInt((uint)Program.wow.MainModule.BaseAddress + (uint)enumValue)).ToString().PadRight(30) + "0x{0:X}", (uint)Program.wow.MainModule.BaseAddress + enumValue);
                    }

                }
                catch { }
            }
        }
    }
}
