using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace samochod
{
    public class Level
    {
        string name {  get; set; }

        // holds level data
        public List<Tile> tileMap {  get; set; }
        // entityList
        public List<EntityTranslator> entities { get; set; }
        // collisionZones
        // winZone
        public List<ZoneTranslator> zones { get; set; }
        public Level() {
        }
    }
    public class EntityTranslator
    {   
        // class for translating entity game object to a json understood format
        public EntityType type { get; set; }
        public float px { get; set; }
        public float py { get; set; }
        public float rotation { get; set; }
        public Model model { get; set; }
        [JsonConstructor]
        public EntityTranslator(EntityType type, float px, float py, float rotation, Model model)
        {
            this.type = type;
            this.px = px;
            this.py = py;
            this.rotation = rotation;
            this.model = model;
        }
    }
    public class ZoneTranslator
    {   
        public EntityType type { get; set; }
        public float px { get; set; }
        public float py { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [JsonConstructor]
        public ZoneTranslator(EntityType type, float px, float py, int Width, int Height)
        {
            this.type = type;
            this.px = px;
            this.py = py;
            this.Width = Width;
            this.Height = Height;
        }
    }
}
