using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace samochod
{
    public class EntityManager
    {
        // list of all spawned entities
        public static List<Entity> entities;
        public static List<Zone> zones;
        // pool of available models to use
        public Dictionary<EntityType, Entity> entityPool;
        public Player player;
        public TextManager text;
        private int globalID = 0;

        public void initEntityManager()
        {
            if (entities == null) { 
                entities = new List<Entity>();
                zones = new List<Zone>();
            }

            else { entities.Clear(); }
        }
        public void initResources()
        {
            entityPool = new Dictionary<EntityType, Entity>()
            {
                { EntityType.car, new Car() },
                { EntityType.player, new Player() },
                //{ EntityType.steeringWheel, new Entity(textures[1] ) },
            };
            
        }
        public EntityManager() 
        {
            initEntityManager();
            initResources(); 
        }
        public void LevelMachen(Level lvl)
        {
            var ent = lvl.entities;
            foreach (var helper in ent)
            {
                switch (helper.type)
                {
                    case EntityType.car:
                        AddCar(helper); break;
                }
            }
            foreach (var zon in lvl.zones)
            {
                AddZone(zon);
            }
        }
        public void AddEntity(Entity e) 
        {
            entities.Add(e);
            Debug.WriteLine("non do AddEntity");

        }
        public void AddZone(Zone obj) 
        {
            zones.Add(obj);
        }
        public void AddZone(ZoneTranslator obj)
        {
            zones.Add(new Zone(obj.type,obj.px,obj.py,obj.Width,obj.Height));
        }
        public Player AddPlayer()
        {
            player = entityPool[EntityType.player].Clone() as Player;
            
            player.init();
            player.ID = globalID++;
            entities.Add(player);
            return player;
        }
        public void AddCar(EntityType model )
        {
            Entity tmpObj = entityPool[model].Clone() as Car;
            tmpObj.ID = globalID++;
            entities.Add(tmpObj);
        }
        public void AddCar(EntityTranslator h)
        {
            Car tmpObj = new Car(h.model, new(h.px, h.py), h.rotation);
            tmpObj.ID = globalID++;
            entities.Add(tmpObj);
        }
        public void AddCar(EntityType model, Vector2 position )
        {

            Entity tmpObj = entityPool[model].Clone() as Car;
            tmpObj.position = position;
            tmpObj.ID = globalID++;
            entities.Add(tmpObj);
        }
        public void Update(GameTime gameTime)
        {
            foreach (var entity in entities) 
            {
                entity.Update(gameTime);
            }
            for (int i = 0; i < entities.Count; i++) 
            {
                if (!entities[i].alive) {
                    entities.RemoveAt(i);
                }
            }
            bool collided = false;
            foreach (var entity in entities) {
                if (entity != player)
                {

                    if (player.hitbox.Intersects(entity?.hitbox))
                    {
                        collided = true;
                    }

                }
                
            }
            text.Write(new Text($"ent: {entities.Count}"));
            if (collided)
            {
                text.Write(new Text("Player touching"));
            }
            else {
                text.Write(new Text("Player chill"));

            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var zon in zones)
            {
                zon.Draw(spriteBatch);
                //spriteBatch.Draw(textures[2], )
            }
            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch);
                //spriteBatch.Draw(textures[2], )
            }
        }
    }
}
