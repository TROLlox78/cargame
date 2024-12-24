﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using samochod.monogame_test;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json.Serialization;



namespace samochod
{
    public class Car : Entity
    {
        protected static Dictionary<Model, Rectangle> models;
        public static List<Model> whiteCars;
        public Model carModel;

        // movement related
        public float speed;
        protected float friction = 0.2f;
        public Vector2 velocity;
        public Vector2 velocityDirection;
        protected float direction = 1;
        public Car() :base()
        {
            if (models == null) { 
                InitModelsDict();
            }
            this.texture = textures[0];
            //width = 91;
            //height = 43;
            //textureBoundry = new Rectangle(452,69, width, height);
            textureBoundry = models[Model.Player];
            position = new Vector2(100, 100);
            width = models[carModel].Width;
            height = models[carModel].Height;
            origin = new Vector2(width/2, height/2);
            Debug.WriteLine("created car model");
        }
        [JsonConstructor]
        public Car(Model carModel, Vector2 position, float rotation) : base()
        {
            if (models == null)
            {
                InitModelsDict();
            }
            this.carModel = carModel;
            this.texture = textures[0];
            textureBoundry = models[carModel];// maybe not allowed with json constructor
            width = models[carModel].Width;
            height = models[carModel].Height;
            origin = new Vector2(width / 2, height / 2);
            this.position = position;
            this.rotation = rotation;
            color = changeColor();
            Debug.WriteLine($"created car{ID}");
        }
        private void InitModelsDict()
        {
            models = new Dictionary<Model, Rectangle> {
                {Model.Player, new  (452,69, 91, 43)},
                {Model.Sports, new  (18,9, 96, 47)},
                {Model.Muscle, new  (128,179, 87, 43)},
                {Model.Luxury, new  (128,237, 86, 43)},
                {Model.Limo, new  (233,10, 135, 46)},
                {Model.Police, new  (451,132, 93, 44)},
                {Model.Taxi, new  (341,67, 99, 47)},
                {Model.Ambulance, new  (447,192, 97, 44)},
                {Model.FireTruck, new  (402,430, 137, 53)},
                {Model.Pickup, new  (345,250, 92, 46)},
                {Model.Bones, new  (341,129, 99, 47)},
                {Model.Sedan, new  (341,191, 99, 47)},
                {Model.Transport,  new  (231,67, 103, 49)},
                {Model.Transport2, new  (231,127, 103, 49)},
                {Model.Transport3, new  (231,189, 103, 49)},
                {Model.Transport4, new  (231,251, 103, 49)},
                {Model.Transport5, new  (231,311, 103, 49)},
                {Model.Transport6, new  (231,369, 103, 49)},
            };
            whiteCars = new List<Model> {
                Model.Luxury,
                Model.Muscle,
                Model.Sports,
                Model.Limo,
                Model.Transport6,
                Model.Pickup,
            };
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (speed != 0)
            {
                speed -= friction;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
            //velocityDirection = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation));
            velocity = direction * speed * velocityDirection;
            position += velocity;

            if (hitbox.InBounds(Input.mousePosition))
            {
                if (Input.IsKeyPressed(Keys.X))
                {
                    changeModel();
                }
            }
        }
        protected Color changeColor()
        {

            if (whiteCars.Contains(carModel))
            {
                return new Color(rand.Next() % 255, rand.Next() % 255, rand.Next() % 255);
            }
            return Color.White;
        }
        protected void changeModel()
        {
            int t = (int)carModel;
            carModel = (Model)(++t % models.Count);
            textureBoundry = models[(Model)carModel];
            width = models[carModel].Width;
            height = models[carModel].Height;
            origin = new Vector2(width / 2, height / 2);
        }

    }
}
