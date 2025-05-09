using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Intrinsics.X86;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace alevel_spacefighter;

public class Ship : CollisionEntity
{

    
    private float engageDistance; // distance required in order to start shooting.
    private float shootCooldown = new Random().Next(700,1200);
    private float currentShootCooldown = new Random().Next(1,1000);

    // Stats
    private int moveSpeed = 100;
    private int health = 200;

    public int team = 0;

    private Texture2D laserTexture;
    
    public Ship(Texture2D setTexture, Vector2 setPosition, Texture2D setLaserTexture, float setHitboxSize, int setTeam = 0) : base(setTexture, setPosition, setHitboxSize, setLaserTexture){
        laserTexture = setLaserTexture;
        team = setTeam;
        hitboxSize = setHitboxSize;
        engageDistance = setHitboxSize;
    }

    protected override void Init() {
        scale = 1; // 2x the size
        
        base.Init();
    }

    public Ship LocateTarget() {

        // pick a target based on distance.
        Entity target = null;
        float currentDistance = float.PositiveInfinity;

        List<Entity> tempEntities = new List<Entity>(EntityManager.entities); // prevents modification during foreach loop which leads to a crash
        
        // loop through each entity and get their distance, skip over this entity. If the entity is closer, then set that entity to be the target.
        foreach (Entity entity in tempEntities) {
            if (entity.GetType() != typeof(Ship)) {continue; }
            Ship ship = entity as Ship;
            if (ship != this && GetDistance(ship.position) < currentDistance && (team != ship.team || ship.team == 0)) {
                target = ship;
                currentDistance = GetDistance(ship.position);
            }
        }

        // check if there is a selected entity, if not: stop.
        if (target == null) {
            return null;
        }


        return (Ship) target;
    }
    public override void Update(GameTime gameTime) {
        // runs once per frame
        
        // locate the target
        Entity target = LocateTarget();

        // final move speed after calculating deltaTime.
        float finalMoveSpeed = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;


        // rotation test
        //rotation += 0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        // look towards the target
        
        // if there is a target:
        if (target != null) {
            rotation = GetDirectionOfOtherEntity(target.position);
            float distance = GetDistance(target.position);

            /* Shooting */
            if (currentShootCooldown >= shootCooldown) {
                // create bullet
                Entity bullet = new ProjectileEntity(laserTexture, position + Vec2Forward(hitboxSize+5), rotation, 5f);
                EntityManager.entities.Add(bullet);

                // set shootCooldown to 0
                currentShootCooldown = 0;
            }

            if (currentShootCooldown < shootCooldown) {
                currentShootCooldown += 1 * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            /* Movement */
            if (distance >= engageDistance) {
                position += Vec2Forward(finalMoveSpeed);
            }
        }
        

        base.Update(gameTime);
    }

    private Vector2 Vec2Forward(float amount) {
        Vector2 final = new Vector2();

        final.X = (float)Math.Cos(rotation) * amount;
        final.Y = (float)Math.Sin(rotation) * amount;

        return final;
    }

    public void OnHit(ProjectileEntity entityCopy) {
        // run when ship should take damage.
        health -= entityCopy.damage;

        if (health <= 0) {
            EntityManager.entities.Remove(this);
        }
        
    }
    public override void OnCollision(Entity entityCopy)
    {
        // is the entity we hit a projectile?
        if (entityCopy is ProjectileEntity) {
            // run on hit
            OnHit((ProjectileEntity) entityCopy);
            EntityManager.entities.Remove(entityCopy);
        }
        base.OnCollision(entityCopy);
    }
}