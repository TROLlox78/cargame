using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace samochod
{
    public class Tile
    {
        public TileID ID {  get; set; }
        public byte flags { get; set; }
        // 0,1  rotation
        // 2    horizontal mirror
        // 3    vertical mirror

        [JsonConstructor] // DO NOT MODIFY FREELY https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/immutability
        public Tile(TileID ID, byte flags)
        {
            this.ID = ID;
            this.flags = flags;
        }
    }
}
