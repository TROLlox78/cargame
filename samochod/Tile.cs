using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class Tile
    {
        public TileID ID;
        public float orientation=0;
        public byte special=0;

        public Tile(TileID tileID)
        {
            ID= tileID;
        }
        public Tile(TileID tileID, float orientation, byte special)
        {
            ID = tileID;
            this.orientation = orientation;
            this.special = special;
        }
    }
}
