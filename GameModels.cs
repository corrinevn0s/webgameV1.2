namespace towerDefense2.Shared.Models
{
    // Клетка игрового поля
    public class GameCell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsPath { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool CanPlaceTower { get; set; }
        public Tower? Tower { get; set; }
        public Enemy? Enemy { get; set; }

        public string Classes
        {
            get
            {
                var classes = "";
                if (IsPath) classes += " path";
                if (IsStart) classes += " start";
                if (IsEnd) classes += " end";
                if (!CanPlaceTower) classes += " blocked";
                if (Tower != null) classes += " has-tower";
                return classes;
            }
        }
    }

    // Башня
    public class Tower
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Level { get; set; } = 1;
        public int Damage { get; set; } = 10;
        public int Range { get; set; } = 3;
        public int FireRate { get; set; } = 1000; // мс
        public DateTime LastShotTime { get; set; }
        public int? TargetEnemyId { get; set; }

        public string ColorClass => Level switch
        {
            1 => "color-blue",
            2 => "color-cyan",
            3 => "color-lightblue",
            _ => "color-white"
        };
    }
    public class Enemy
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string Type { get; set; } = "normal";
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Speed { get; set; } = 1;
        public int Reward { get; set; } = 10;
        public int PathIndex { get; set; } = 0;

        public int HealthPercent => (Health * 100) / MaxHealth;

        public string ColorClass => Type switch
        {
            "fast" => "color-yellow",
            "tank" => "color-darkgray",
            "boss" => "color-purple",
            _ => "color-red"
        };
    }

    public class PathPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}