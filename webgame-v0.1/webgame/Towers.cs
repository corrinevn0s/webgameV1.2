using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace webgame
{
    public enum TowerType
    {
        Archer,
        IceMage,
        FireMage,
        Witch
    }

    public class Tower
    {
        public TowerType Type { get; set; }
        public Vector2 Position { get; set; }
        public int Damage { get; set; }
        public float Range { get; set; }
        public float FireRate { get; set; }
        public float Cooldown { get; set; }
        public int Cost { get; set; }
        public Color Color { get; set; }
        public string Name { get; set; }

        public Tower(TowerType type, Vector2 position)
        {
            Type = type;
            Position = position;

            switch (type)
            {
                case TowerType.Archer:
                    Damage = 15; Range = 150; FireRate = 2.0f; Cost = 50;
                    Color = Color.Green; Name = "Archer"; break;

                case TowerType.IceMage:
                    Damage = 8; Range = 120; FireRate = 1.0f; Cost = 80;
                    Color = Color.Cyan; Name = "Ice Mage"; break;

                case TowerType.FireMage:
                    Damage = 20; Range = 100; FireRate = 0.7f; Cost = 100;
                    Color = Color.OrangeRed; Name = "Fire Mage"; break;

                case TowerType.Witch:
                    Damage = 10; Range = 130; FireRate = 1.5f; Cost = 120;
                    Color = Color.Purple; Name = "Witch"; break;
            }

            Cooldown = 0;
        }

        public void Update(GameTime gameTime, List<Enemy> enemies, GameState gameState)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Cooldown -= deltaTime;

            if (Cooldown > 0) return;

            Enemy target = null;
            float minDistance = float.MaxValue;

            foreach (var enemy in enemies)
            {
                if (!enemy.IsAlive) continue;

                float distance = Vector2.Distance(enemy.Position, Position);

                if (distance <= Range && distance < minDistance)
                {
                    minDistance = distance;
                    target = enemy;
                }
            }

            if (target != null)
            {
                target.Health -= Damage;

                if (!target.IsAlive)
                {
                    gameState.Gold += target.GoldReward;
                }

                Cooldown = 1.0f / FireRate;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            Rectangle towerRect = new Rectangle(
                (int)Position.X - 20,
                (int)Position.Y - 20,
                40, 40);

            spriteBatch.Draw(pixel, towerRect, Color);
        }
    }
}