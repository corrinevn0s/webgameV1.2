using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace webgame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _pixel;
        private SpriteFont _font;
        private GameState _gameState;
        private TowerType _selectedTower = TowerType.Archer;
        private int _selectedSlot = -1;
        private bool _selectedSideIsLeft = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            _gameState = new GameState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _pixel = new Texture2D(GraphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            //Проверка работы шрифта

            try
            {
                _font = Content.Load<SpriteFont>("Arial");
            }
            catch
            {
                _font = null;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_gameState.IsGameOver)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    _gameState = new GameState();
                }
                return;
            }

            var mouseState = Mouse.GetState();
            var keyboardState = Keyboard.GetState();

            // Выбор башни
            if (keyboardState.IsKeyDown(Keys.D1)) _selectedTower = TowerType.Archer;
            if (keyboardState.IsKeyDown(Keys.D2)) _selectedTower = TowerType.IceMage;
            if (keyboardState.IsKeyDown(Keys.D3)) _selectedTower = TowerType.FireMage;
            if (keyboardState.IsKeyDown(Keys.D4)) _selectedTower = TowerType.Witch;
        }
    }
}
