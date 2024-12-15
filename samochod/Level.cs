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
        public List<EntityHelper> entities { get; set; }
        // collisionZones
        // winZone
        public Level() {
        }
    }
    public class EntityHelper
    {   // too big a bother to clown with the polymorphism json syntax
        // class used to keep entity values, not the best way of doing ths
        public EntityType type { get; set; }
        public float px { get; set; }
        public float py { get; set; }
        public float rotation { get; set; }
        public Model model { get; set; }
        [JsonConstructor]
        public EntityHelper(EntityType type, float px, float py, float rotation, Model model)
        {
            this.type = type;
            this.px = px;
            this.py = py;
            this.rotation = rotation;
            this.model = model;
        }
    }
}
