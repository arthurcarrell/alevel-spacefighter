using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_spacefighter;

public class Ship : Entity
{

    private int moveSpeed = 200;
    public Ship(Texture2D setTexture, Vector2 setPosition) : base(setTexture, setPosition){}

    protected override void Init() {
        scale = 2; // 2x the size
        
        base.Init();
    }
    public override void Update(GameTime gameTime) {
        // runs once per frame
        // just to test, make ship that can be controlled with WASD, dont care about deltatime rn
        KeyboardState keyboardState = Keyboard.GetState();

        // final move speed after calculating deltaTime.
        float finalMoveSpeed = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (keyboardState.IsKeyDown(Keys.Down)) { position.Y += finalMoveSpeed; }
        if (keyboardState.IsKeyDown(Keys.Up)) { position.Y -= finalMoveSpeed; }
        if (keyboardState.IsKeyDown(Keys.Left)) { position.X -= finalMoveSpeed; }
        if (keyboardState.IsKeyDown(Keys.Right)) { position.X += finalMoveSpeed; }

        base.Update(gameTime);
    }
}