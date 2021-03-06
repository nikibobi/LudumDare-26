﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.Window;

namespace LD26.Entitys.Components
{
    class ColiderComponent : Component
    {
        public ColiderComponent(FloatRect hitbox)
        {
            Hitbox = hitbox;
        }

        public FloatRect Hitbox { get; set; }

        public event OnColide Colides;

        public override void Update(float dt)
        {
            base.Update(dt);
            Hitbox = new FloatRect(
                MyEntity.Transform.Position.X - MyEntity.Transform.Origin.X,
                MyEntity.Transform.Position.Y - MyEntity.Transform.Origin.Y, 
                Hitbox.Width, 
                Hitbox.Height);
        }

        public override void Draw(RenderTarget rt, RenderStates rs)
        {
            base.Draw(rt, rs);

            RectangleShape box = new RectangleShape(new Vector2f(Hitbox.Width, Hitbox.Height));
            box.Position = new Vector2f(Hitbox.Left, Hitbox.Top);
            box.OutlineThickness = 1;
            box.OutlineColor = Color.Red;
            box.FillColor = new Color(0, 0, 0, 0);
            rt.Draw(box);
        }

        public void Colide(ColiderComponent colider, FloatRect overlap)
        {
            var handler = Colides;
            if (handler != null)
                handler(colider, overlap);
        }

        public delegate void OnColide(ColiderComponent colider, FloatRect overlap);
    }
}
