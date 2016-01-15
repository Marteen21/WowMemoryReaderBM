using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowMemoryReaderBM.Constants;
using WowMemoryReaderBM.Bots;
using WowMemoryReaderBM.Objects;

namespace WowMemoryReaderBM.Models {
    public class Spell {
        uint id;
        int keybind;
       //Bela
        public uint ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
        public Spell(uint i) {
            this.ID = i;

        }
        public Spell(uint i, Const.WindowsVirtualKey kb) {
            this.ID = i;
            this.keybind = (int)kb;
        }
        public void SendCast() {
            SendKeys.Send(this.keybind);
        }
        public bool ReCast(GameObject go) {
            uint SpellPending = Program.wow.ReadUInt((uint)Program.wow.MainModule.BaseAddress + (uint)Constants.Const.Globals.SpellIsPending);
            if (!go.HasBuff(this.ID) && SpellPending==0) {
                this.SendCast();
                return true;
            }
            else {
                return false;
            }
        }
        public bool IfCast(GameObject go) {
            if (go.HasBuff(this.ID)) {
                this.SendCast();
                return true;
            }
            return false;
        }
    }
}
