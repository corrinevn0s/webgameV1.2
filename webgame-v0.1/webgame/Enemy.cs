using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace webgame
{
    public enum EnemySide
    {
        Left,
        Right
    }

    public class Enemy
    {
        public EnemySide Side { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public float Speed { get; set; }
        public int GoldReward { get; set; }
        public Vector2 Position { get; set; }
        public bool IsAlive => Health > 0;
        public bool ReachedEnd => Position.Y >= 550;

        public Rectangle Bounds => new Rectangle(
            (int)Position.X - 15,
            (int)Position.Y - 15,
            30, 30);

        public Enemy(EnemySide side, int waveNumber, Vector2 startPosition)
        {
            Side = side;
            Position = startPosition;

            MaxHealth = 30 + waveNumber * 10;
            Health = MaxHealth;
            Speed = 1.0f + waveNumber * 0.1f;
            GoldReward = 10 + waveNumber * 3;
        }

        public void Update()
        {
            Position = new Vector2(Position.X, Position.Y + Speed);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
        {
            Color enemyColor = (Side == EnemySide.Left) ? Color.Red : Color.Blue;
            spriteBatch.Draw(pixel, Bounds, enemyColor);

            float healthPercent = (float)Health / MaxHealth;
            Rectangle healthBar = new Rectangle(
                (int)Position.X - 15,
                (int)Position.Y - 20,
                (int)(30 * healthPercent),
                5);

            spriteBatch.Draw(pixel, healthBar, Color.Green);
        }
    }
}