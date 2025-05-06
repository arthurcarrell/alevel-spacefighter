using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_spacefighter;


/*

This file is looking small!

Thats intentional. All that this file is supposed to do is call functions in other files.
Ive gone with an Entity based design idea,
Everything (apart from the background) that you see on the screen is an Entity,
and all of the code for the entities is designed to be handled in that entity class.
Instead of setting up rendering here, each entity has its own rendering function that this main file calls,
Instead of updates being ran here, each Entity has its own Update() function that is called here
Entities can access each other by using the one global variable, entities, which simply contains a list of all the entities and is responsible for all of the logic.
If an entity is created but is not in entities, it will not do anything, nor will it even render.

TLDR:
All this file does is run functions for everything else.
*/
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D testTexture;
    Entity testEntity;

    /* Every single entity created needs to be added to this list, this list is responsible
    for making the entity render, and it calls each entities Update() and Render() function. */
    public List<Entity> entities = new List<Entity>();


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        testTexture = Content.Load<Texture2D>("ship");

        // TODO: use this.Content to load your game content here
        testEntity = new Ship(testTexture, new Vector2(0,0));
        entities.Add(testEntity);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        UpdateEntities(gameTime);

        base.Update(gameTime);
    }

    protected void DrawEntities(SpriteBatch spriteBatch) {
        foreach (Entity entity in entities) {
            entity.Render(spriteBatch);
        }
    }

    protected void UpdateEntities(GameTime gameTime) {
        foreach (Entity entity in entities) {
            entity.Update(gameTime);
        }
    }
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        DrawEntities(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
