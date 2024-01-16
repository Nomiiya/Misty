using Godot;
using System;
using System.Collections;

public partial class Projectile : CharacterBody2D
{
    private int speed = 300;
    private int damage = 10;
    KinematicCollision2D collision {get;set;}

    public override void _Ready()
	{
        
    }

    public override void _Process(double delta)
    {
        collision = MoveAndCollide(Velocity * (float)delta);
    }

    public void Shoot( Godot.Vector2 enemyPos, Godot.Vector2 pos){
		Godot.Vector2 direction = ( enemyPos - pos).Normalized();
		Velocity = direction * speed;
        LookAt(enemyPos);

	}

    public void OnHitBoxEntered(Node2D body){
        if(body.IsInGroup("Enemy")){
            GetNode<Zombie>(body.GetPath()).DamageTaken(damage);
            QueueFree();
        }
    }
}
