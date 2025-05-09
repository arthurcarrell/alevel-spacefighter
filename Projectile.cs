using System;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_spacefighter;

public class ProjectileEntity : Entity
{

    public int moveSpeed = 20;
    public int damage = 10;
    protected const int TOTAL_LIFETIME = 2000; // the amount of milliseconds until this projectile is deleted
    protected float currentLifetime = 0;
    public ProjectileEntity(Texture2D setTexture, Vector2 setPosition, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
    }

    public override void Update(GameTime gameTime)
    {
        position += Vec2Forward(moveSpeed);
        base.Update(gameTime);

        currentLifetime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        if (currentLifetime >= TOTAL_LIFETIME) {
            EntityManager.entities.Remove(this);
        }
    }

    private Vector2 Vec2Forward(int amount) {
        Vector2 final = new Vector2();

        final.X = (float)Math.Cos(rotation) * amount;
        final.Y = (float)Math.Sin(rotation) * amount;

        return final;
    }
}