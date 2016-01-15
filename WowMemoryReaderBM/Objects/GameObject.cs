using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
//using WowMemoryReaderBM.Database;

namespace WowMemoryReaderBM.Objects {
    public class GameObject {
        private uint baseAddress;
        private UInt64 guid;
        private uint descriptorArrayAddress;
        private uint buffArrayAddress;
        private uint buffOffsetArrayAddress;
        private bool isMoving=false;
        private uint movementArrayAddress;
        private uint healthpercent;
        private uint manapercent;
        private List<uint> buffIDs;
        #region provertys
        public uint BaseAddress {
            get {
                return baseAddress;
            }

            set {
                baseAddress = value;
            }
        }

        public ulong Guid {
            get {
                return guid;
            }

            set {
                guid = value;
            }
        }


        public uint DescriptorArrayAddress {
            get {
                return descriptorArrayAddress;
            }

            set {
                descriptorArrayAddress = value;
            }
        }

        public uint BuffArrayAddress {
            get {
                return buffArrayAddress;
            }

            set {
                buffArrayAddress = value;
            }
        }

        public List<uint> BuffIDs
        {
            get
            {
                return buffIDs;
            }

            set
            {
                buffIDs = value;
            }
        }

        public uint BuffOffsetArrayAddress
        {
            get
            {
                return buffOffsetArrayAddress;
            }

            set
            {
                buffOffsetArrayAddress = value;
            }
        }

        public uint MovementArrayAddress
        {
            get
            {
                return movementArrayAddress;
            }

            set
            {
                movementArrayAddress = value;
            }
        }

        public bool IsMoving
        {
            get
            {
                return isMoving;
            }

            set
            {
                isMoving = value;
            }
        }

        public uint Manapercent
        {
            get
            {
                return manapercent;
            }

            set
            {
                manapercent = value;
            }
        }

        public uint Healthpercent
        {
            get
            {
                return healthpercent;
            }

            set
            {
                healthpercent = value;
            }
        }


        #endregion
        public GameObject() {
            this.BaseAddress = 0;
            this.Guid = 0;
            this.DescriptorArrayAddress = 0;
        }

        public GameObject(uint baddr) { //Constructor from base Address
            this.BaseAddress = baddr;
            this.Guid = Program.wow.ReadUInt64(baddr + (uint)Constants.Const.ObjectManager.LocalGUID);
            this.DescriptorArrayAddress = Program.wow.ReadUInt(this.BaseAddress + 0xC) + 0x10;
            this.BuffArrayAddress = Program.wow.ReadUInt(this.baseAddress + 0xe9c) + 0x4;
            this.BuffOffsetArrayAddress = baddr + 0xe98;
            this.BuffIDs = new List<uint>();
        }
        public GameObject(UInt64 gid) { //Constructor from GUID
            this.Guid = gid;
            if (gid == 0) {     //If GUID zero, return blank Object
                this.BaseAddress = 0;
                this.DescriptorArrayAddress = 0;
                this.BuffArrayAddress = 0;
                this.BuffOffsetArrayAddress = 0;
                this.MovementArrayAddress = 0;
                this.BuffIDs = new List<uint>();
                return;
            }
            else {  //Iterate through the objects (linked list) from first object till the GUID matches
                GameObject TempObject = new GameObject();
                TempObject.BaseAddress = Program.FirstObject.BaseAddress;
                try {
                    while (TempObject.BaseAddress != 0) {
                        TempObject.Guid = Program.wow.ReadUInt64(TempObject.BaseAddress + (uint)Constants.Const.ObjectManager.LocalGUID);
                        if (TempObject.Guid == this.guid) {
                            this.BaseAddress = TempObject.BaseAddress;
                            this.DescriptorArrayAddress = Program.wow.ReadUInt(this.BaseAddress + 0xC) + 0x10;
                            this.BuffArrayAddress = Program.wow.ReadUInt(this.baseAddress + 0xe9c) + 0x4;
                            this.BuffOffsetArrayAddress = this.BaseAddress + 0xe98;
                            this.MovementArrayAddress = Program.wow.ReadUInt(this.BaseAddress + (uint)Const.Movement.Pointer)+(uint)Const.Movement.Offset;
                            this.BuffIDs = new List<uint>();
                            return;
                        }
                        else {
                            TempObject.BaseAddress = Program.wow.ReadUInt(TempObject.BaseAddress + (uint)Constants.Const.ObjectManager.NextObject);
                        }
                    }
                }
                catch {
                    this.BaseAddress = 0;
                    this.DescriptorArrayAddress = 0;
                    this.buffArrayAddress = 0;
                    this.BuffIDs = new List<uint>();
                    return;
                }
            }
        }
        public void RefreshBuffIDs() {
            uint temp = 1;
            uint i = 0;
            this.BuffIDs.Clear();
            this.IsMoving = (Program.wow.ReadByte(this.MovementArrayAddress)!=0x00);
            uint addr = 0;
            if(Program.wow.ReadUInt(this.BuffOffsetArrayAddress)!=0) {
                addr = this.BuffOffsetArrayAddress;
            }
            else {
                addr = this.BuffArrayAddress;
            }
            while (temp != 0) {
                temp = Program.wow.ReadUInt(addr + (0x08 * i));
                i++;
                if (temp != 0) {
                    this.BuffIDs.Add(temp);
                }
            }
        }
        public void Refresh() {
            this.RefreshHPMana();
            this.RefreshBuffIDs();
        }
        public void RefreshHPMana() {
            uint hp =Program.wow.ReadUInt(this.DescriptorArrayAddress + (uint)Const.descriptors.Health);
            uint hpmax = Program.wow.ReadUInt(this.DescriptorArrayAddress + (uint)Const.descriptors.MaxHealth);
            uint mana = Program.wow.ReadUInt(this.DescriptorArrayAddress + (uint)Const.descriptors.Mana);
            uint manamax = Program.wow.ReadUInt(this.DescriptorArrayAddress + (uint)Const.descriptors.MaxMana);
            this.Manapercent = ((100*mana) / manamax);
            this.Healthpercent = ((100 * hp) / hpmax);
        }
        public bool HasBuff(uint buffid) {
            foreach(uint bid in this.BuffIDs) {
                if (bid == buffid) {
                    return true;
                }
            }
            return false;
        }

    }
}
