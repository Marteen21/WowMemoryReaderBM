using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowMemoryReaderBM.Models {
    public class Spell {
        uint id;
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
    }
}
