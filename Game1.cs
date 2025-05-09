using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    Entity testEntity;
    public static Texture2D shipTexture;
    public static Texture2D laserTexture;
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1080;
        _graphics.PreferredBackBufferHeight = 720;
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
        shipTexture = Content.Load<Texture2D>("ship");
        laserTexture = Content.Load<Texture2D>("pixel");


        // TODO: use this.Content to load your game content here
        Random rnd = new Random();
        for (int i=0; i < 30; i++) {
            Ship testEntity2 = new Ship(shipTexture, new Vector2(rnd.Next(300),rnd.Next(700)), laserTexture, 20, 1);
            EntityManager.entities.Add(testEntity2);
        }
        for (int i=0; i < 30; i++) {
            Ship testEntity2 = new Ship(shipTexture, new Vector2(rnd.Next(500)+600,rnd.Next(700)), laserTexture, 20, 2);
            EntityManager.entities.Add(testEntity2);
        }
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
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities) {
            entity.Render(spriteBatch);
        }
    }

    protected void UpdateEntities(GameTime gameTime) {
        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        foreach (Entity entity in tempEntities) {
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