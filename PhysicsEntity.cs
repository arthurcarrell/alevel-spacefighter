using System;
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


    protected virtual void PhysicsStep() {
        // code physics stuff here
    }


}