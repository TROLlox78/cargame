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
        public List<Entity> entities;
        // pool of available models to use
        public Dictionary<EntityType, Entity> entityPool;
        List<Texture2D> textures;
        public Player player;

        private int globalID = 0;

        public void initEntityManager()
        {
            if (entities == null) { entities = new List<Entity>(); }

            else { entities.Clear(); }
        }
        public void initResources()
        {
            entityPool = new Dictionary<EntityType, Entity>()
            {
                { EntityType.car, new Car(textures[0] ) },
                { EntityType.player, new Player(textures[0] ) },
                //{ EntityType.steeringWheel, new Entity(textures[1] ) },
            };
            
        }
        public EntityManager(List<Texture2D> textures) 
        {
            this.textures = textures;
            initEntityManager();
            initResources(); 
        }

        public void AddEntity(EntityType entityType) 
        {
            Debug.WriteLine("IDg");

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
            foreach (var entity in entities) {
                if (entity != player)
                {

                    player.hitbox.Intersects(entity?.hitbox);
                }
            }

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                
                entity.Draw(spriteBatch);
                //spriteBatch.Draw(textures[2], )
            }
        }
    }
}
