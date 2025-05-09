using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace alevel_spacefighter;

public class CollisionEntity : Entity
{

    public static float hitboxSize;
    public static bool isHitboxEnabled = true;

    private Texture2D hitboxTexture;
    public CollisionEntity(Texture2D setTexture, Vector2 setPosition, float setHitboxSize, Texture2D setHitboxTexture, float setRotation = 0, float setScale = 1) : base(setTexture, setPosition, setRotation, setScale)
    {
        hitboxSize = setHitboxSize;
        hitboxTexture = setHitboxTexture;
    }

    public virtual void OnCollision(Entity entityCopy) {
        // do nothing, this exists to be overidden.
    }
    public void HitboxUpdate(GameTime gameTime) {
        List<Entity> entities = new List<Entity>(EntityManager.entities);

        foreach (Entity entity in entities) {
            if (GetDistance(entity.position) <= hitboxSize && entity != this) {
                //Console.WriteLine($"{this}: Something entered hitbox! My POS: ({position.X} , {position.Y}) - Collider POS: ({entity.position.X} {entity.position.Y})");
                OnCollision(entity);
            }
        }
    }
    public override void Update(GameTime gameTime)
    {
        // run hitbox update
        if (isHitboxEnabled) { HitboxUpdate(gameTime); }

        // base.Update() must be called on any class that inherits this.
        base.Update(gameTime);
    }

    public override void Render(SpriteBatch spriteBatch)
    {
        if (EntityManager.showHitboxes) {
            spriteBatch.Draw(hitboxTexture, position, null, Color.White, 0, new Vector2(hitboxTexture.Width / 2f, hitboxTexture.Height / 2f), hitboxSize, SpriteEffects.None, 0);
        }
        base.Render(spriteBatch);
    }

    protected float GetDirectionOfOtherEntity(Vector2 otherPosition) {
        double dx = otherPosition.X-position.X; double dy = otherPosition.Y-position.Y;

        float otherDirection = (float) Math.Atan2(dy, dx);

        return otherDirection;
    }

    protected float GetDistance(Vector2 otherPosition) {
        // use pythagorus theorem to calculate the hypotenuse
        //float answer = (float) Math.Sqrt(((otherPosition.X - position.X) * (otherPosition.X - position.X)) + ((otherPosition.Y - position.Y) * (otherPosition.Y - position.Y)));
        
        float answer = Vector2.Distance(position, otherPosition);
        
        return answer;
            
    }
}