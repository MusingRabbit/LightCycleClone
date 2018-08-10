using LightCycleClone.AI;
using LightCycleClone.GameObjects;
using LightCycleClone.GameObjects.Character;
using LightCycleClone.GameObjects.World;
using LightCycleClone.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LightCycleClone
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class LinearCombatGame : Game
    {
        private Renderer _renderer;

        private ResourceController _resources;
        private SpriteBatch _gameSpriteBatch;
        private SpriteBatch _uiSpriteBatch;

        private Player _player1;
        private PlayerAgent _agent1;

        private Player _player2;
        private ReflexAgent _agent2;

        private Player _player3;
        private ReflexAgent _agent3;

        private TimeSpan _deltaTime;
        private InputController _controller;

        private const int TargetWidth = 800;
        private const int TargetHeight = 600;

        private GraphicsDeviceManager _deviceManager;

        private TileMap _tileMap;

        private GameWorld _world;

        public Color Colour { get; private set; }

        public LinearCombatGame()
        {
            Content.RootDirectory = "Content";

            _deviceManager = new GraphicsDeviceManager(this);
            _deviceManager.PreferredBackBufferWidth = 800;
            _deviceManager.PreferredBackBufferHeight = 600;

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _resources = new ResourceController(Content);
            _renderer = new Renderer(_resources, _deviceManager);

            _player1 = new Player(new Point(2,2),  Direction.East, Color.LightSteelBlue);
            //this.player2 = new Player(new Point(48, 26), Direction.North, Color.IndianRed);
            //this.player3 = new Player(new Point(11, 25), Direction.West, Color.Cyan);

            _controller = new InputController();
            
            _tileMap = new TileMap();
            _tileMap.CreateLevel(50);

            _world = new GameWorld();
            _world.AddPlayer(_player1);
            //this.world.AddPlayer(this.player2);
            //this.world.AddPlayer(this.player3);
            _world.SetTileMap(_tileMap);
            _world.Initialise();

            _agent1 = new PlayerAgent(_player1);
            _agent2 = new ReflexAgent(_player2);
            _agent3 = new ReflexAgent(_player3);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _gameSpriteBatch = new SpriteBatch(GraphicsDevice);
            _uiSpriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _resources.SetTexture2D("tileTexture", Texture2DHelper.CreateWhiteTexture2D(GraphicsDevice, 32, 32));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            if (keyboardState.IsKeyDown(Keys.R))
                Initialize();

            _controller.Update(Keyboard.GetState());
            
            _deltaTime += gameTime.ElapsedGameTime;

            if (_deltaTime.TotalSeconds > 0.5)
            {
                _agent1.Update(_world);
                //this.agent2.Update(new GameWorld(this.world));
                //this.agent3.Update(new GameWorld(this.world));

                _player1.SetAction(_controller.GetPlayerAction());

                _world.Update();
                _deltaTime = TimeSpan.Zero;
            }

            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _gameSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);
            _renderer.Render(_gameSpriteBatch, _world);
            _gameSpriteBatch.End();

            //this.uiSpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, null);

            //this.uiSpriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }
    }
}
