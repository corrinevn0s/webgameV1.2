using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace webgame
{
    public class GameState
    {
        public int Gold { get; set; } = 150;
        public int PlayerHealth { get; set; } = 100;
        public int CurrentWave { get; set; } = 1;
        public bool IsWaveActive { get; set; }
        public bool IsGameOver => PlayerHealth <= 0;

        public List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public List<Tower> Towers { get; set; } = new List<Tower>();

        public List<Vector2> TowerSlotsLeft { get; set; } = new List<Vector2>();
        public List<Vector2> TowerSlotsRight { get; set; } = new List<Vector2>();
        public List<bool> TowerSlotsOccupiedLeft { get; set; } = new List<bool>();
        public List<bool> TowerSlotsOccupiedRight { get; set; } = new List<bool>();

        private float spawnTimer = 0;
        private int enemiesToSpawn = 0;
        private Random random = new Random();

        public GameState()
        {
            // Инициализируем места для башен
            for (int i = 0; i < 5; i++)
            {
                TowerSlotsLeft.Add(new Vector2(100, 100 + i * 80));
                TowerSlotsOccupiedLeft.Add(false);
            }

            for (int i = 0; i < 5; i++)
            {
                TowerSlotsRight.Add(new Vector2(700, 100 + i * 80));
                TowerSlotsOccupiedRight.Add(false);
            }
        }

        public void StartWave()
        {
            if (IsWaveActive || IsGameOver) return;

            IsWaveActive = true;
            enemiesToSpawn = 3 + CurrentWave * 2;
            spawnTimer = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (IsGameOver || !IsWaveActive) return;

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Спавн врагов
            if (enemiesToSpawn > 0)
            {
                spawnTimer += deltaTime;
                if (spawnTimer >= 0.8f)
                {
                    EnemySide side = (random.Next(2) == 0) ? EnemySide.Left : EnemySide.Right;
                    Vector2 startPosition = (side == EnemySide.Left)
                        ? new Vector2(50, -30)
                        : new Vector2(750, -30);

                    Enemies.Add(new Enemy(side, CurrentWave, startPosition));
                    enemiesToSpawn--;
                    spawnTimer = 0;
                }
            }
        }
    }
}
