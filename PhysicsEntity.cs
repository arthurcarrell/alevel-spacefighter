using System;
using System.Data;
using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace alevel_spacefighter;

public class PhysicsEntity : CollisionEntity
{
    
    public const int MAX_MOVESPEED = Int32.MaxValue;
    public const float ACCELERATION_SPEED = 0.5f;
    public const float FRICTION = 0.5f;

    public Vector2 currentVelocity = new Vector2();
    public PhysicsEntity(Texture2D setTexture, Vector2 setPosition, float setHitboxSize, Texture2D setHitboxTexture, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setHitboxSize, setHitboxTexture, setRotation, setScale)
    {
    }

    protected void Accelerate(float direction, float amount=ACCELERATION_SPEED) {
        currentVelocity += Vec2Forward(direction,amount);
    }
    protected virtual void PhysicsStep() {
        // code physics stuff here

        // move based on currentVelocity
        position.X += currentVelocity.X * FRICTION;
        position.Y += currentVelocity.Y * FRICTION;

        // reduce currentVelocity
        currentVelocity.X -= FRICTION;
        currentVelocity.Y -= FRICTION;

    }

    private Vector2 Vec2Forward(float rotation, float amount) {
        Vector2 final = new Vector2();

        final.X = (float)Math.Cos(rotation) * amount;
        final.Y = (float)Math.Sin(rotation) * amount;

        return final;
    }
}