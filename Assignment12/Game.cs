using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;

namespace Assignment12;
public class Game : Microsoft.Xna.Framework.Game
{
    // All vars now private and up here as they should be!
    private static GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Grid gameGrid;
    // Set the position of the camera in world space, for our view matrix.
    private Vector3 cameraPosition = new Vector3(0.0f, 350.0f, 350.0f);
    // The aspect ratio determines how to scale 3d to 2d projection.
    private float aspectRatio;

    // Text font stuff to use text with monogame
    private SpriteFont font;
    private string message = "";
    private bool gameOver = false;

    // Model to be drawn
    private MyModel[] m = new MyModel[4];

    // Camera vectors
    private Vector3 cameraTarget = Vector3.Zero;
    private Vector3 cameraUpDirection = Vector3.Up;

    // InputHandler moment
    InputHandler inputHandler = new InputHandler();

    public Game()
    {
        // Setup things that need to be setup
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Call super intialize method
        base.Initialize();
    }

    // Load content
    protected override void LoadContent()
    {

        // Flyweight pattern: create one of each mesh, and reuse
        // These 4 meshes in the grid.  
        // Load each mesh one at a time, 4 types of mesh objects
        // All mesh objects displayed, will render from one of these 4
        MyModel.setupGraphics(_graphics);
        m[(int)GridVal.center] = new MyModel(Content.Load<Model>("Models\\Z"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)GridVal.X] = new MyModel(Content.Load<Model>("Models\\X"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)GridVal.O] = new MyModel(Content.Load<Model>("Models\\O"), Vector3.Zero, Vector3.Zero, Color.Yellow);
        m[(int)GridVal.dot] = new MyModel(Content.Load<Model>("Models\\dot"), Vector3.Zero, Vector3.Zero, Color.Yellow);

        // Setup grid, ratio, etc.
        aspectRatio = (float)_graphics.GraphicsDevice.Viewport.Width / _graphics.GraphicsDevice.Viewport.Height;
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        font = Content.Load<SpriteFont>("Fonts/font");
        gameGrid = new Grid(m);
    }

    // Update loop
    protected override void Update(GameTime gameTime)
    {
        // Do the same as before, but now while not as a game over    
        if (!gameOver)
        {
            inputHandler.HandleInput(gameGrid, out gameOver, out message);
        }
        else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
    
    // Draw loop
    protected override void Draw(GameTime gameTime)
    {
       // Setup graphics and spriteBatch
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();

        // Draw the grid from the class
        gameGrid.DrawGrid(cameraPosition, aspectRatio, cameraTarget, cameraUpDirection);
        
        // Draw the text if gameOver is true
        if (gameOver)
        {
            Vector2 textSize = font.MeasureString(message);
            Vector2 position = new Vector2((_graphics.PreferredBackBufferWidth - textSize.X) / 2);
            _spriteBatch.DrawString(font, message, position, Color.Bisque);
        }

        // End spriteBatch
        _spriteBatch.End();

        // Super call
        base.Draw(gameTime);
    }
}