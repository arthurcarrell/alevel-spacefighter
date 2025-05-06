using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace alevel_spacefighter;

public class Entity
{
    /*
    This class isn't designed to be used by itself, instead, it's designed to be inherited and exists as a base for all objects in the game.
    This means that all rendering code is universally editable here, and basic functions are abstracted so they dont need to be thought about
    when making an object.
    */

    // RENDER STUFF
    protected Texture2D texture;
    public Boolean shouldRender = true;

    public float scale = 1;
    public float rotation = 0;
    public int layer = 0;

    // Coordinate Stuff
    public Vector2 position;

    
    /* MAIN FUNCTIONS */
    protected virtual void Init() {
        // Run on Entity creation
    }

    public virtual void Update(GameTime gameTime) {
        // Run each frame
    }


    /* GETTERS */
    public Texture2D GetTexture2D() {
        return texture;
    }

    // Render, this is run every frame and draws the texture, with the sprite drawn in the center, not in the corner.
    public void Render(SpriteBatch spriteBatch) {
        if (shouldRender) {
            // calculate x and y so that the sprite is at the center of the screen
            // TODO: Add rotation support
            float finalX = texture.Width / 2 + position.X;
            float finalY = texture.Height / 2 + position.Y;
            // render onto the screen
            spriteBatch.Draw(texture, position, null, Color.White, rotation, new Vector2(texture.Height / 2, texture.Width / 2), scale, SpriteEffects.None, layer);
        }
    }

    // constructor
    public Entity(Texture2D setTexture, Vector2 setPosition) {
        texture = setTexture;
        position = setPosition;

        // Run init
        Init();
    }
}