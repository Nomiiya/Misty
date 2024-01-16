using Godot;
using System;

public partial class Zombie : CharacterBody2D
{
    // Nodes
    AnimationNodeStateMachinePlayback  stateMachine;
    session_data sesData;
    Label HP;

    Node2D AttackTarget;

    public int currentHealth = 10;
    public int maxHealth = 10;
    float speed = 50;
    int damage = 10;
    int pointWorth = 1;
    Godot.Collections.Array<Area2D> InRange = new Godot.Collections.Array<Area2D>();
    Godot.Vector2 target = new Godot.Vector2(0,0);

    public override void _Ready(){
        sesData = GetNode<session_data>("/root/SessionData");
        stateMachine = (AnimationNodeStateMachinePlayback)GetNode("AnimationTree").Get("parameters/playback");
        AttackTarget = new Area2D();

        //Get Reused Children
        HP = GetNode<Label>("HP");
    }

    public override void _Process(double delta)
	{   
        //Move to target
        if(target.DistanceTo(GlobalPosition) > 32){
            MoveAndCollide((target - GlobalPosition).Normalized());
        }

        //Update HP every Frame
        HP.Text = currentHealth.ToString() + " / " + maxHealth.ToString();
        if(currentHealth <= 0){ OnDeath();}
	}

    public void OnRangeAreaEntered(Node2D body){
        if(body.IsInGroup("EnemyAttackable")){
            if(body.GlobalPosition.DistanceTo(GlobalPosition) < target.DistanceTo(GlobalPosition)){
            target = body.GlobalPosition;
            }
        }
    }

    // Something in range to be attacked
    public void OnDetectInRange(Node2D body){
        if(body.IsInGroup("EnemyAttackable")){
            stateMachine.Travel("attack");
            AttackTarget = body;
        }
    }

    public void OnDetectExited(Node2D body){
        if(body == AttackTarget){
            stateMachine.Travel("idle");
            AttackTarget = new Area2D();
            target = new Godot.Vector2(0,0);
        }
    }

    // If we hit anything with the attack area do this
    public void OnAttackHit(Node2D body){
        if(body.IsInGroup("Building")){
            GetNode<WorkerBuilding>(body.GetPath()).DamageTaken(damage);
        }
        if(body.IsInGroup("LuminiteMine")){
            GetNode<LuminiteMine>(body.GetPath()).DamageTaken(damage);
        }
        if(body.IsInGroup("Archer")){
            GetNode<ArcherTower>(body.GetPath()).DamageTaken(damage);
        }
    }


    // Animation Functions
    public void Hit(){
        GetNode<Area2D>("Attack").Monitoring = true;
    }

    public void EndOfHit(){
        //GetNode<Area2D>("Attack").Monitoring = false;
    }
    public void StartIdle(){
        //stateMachine.Travel("idle");
    }


    /* FUNCTION FOR THIS SCENE*/
    public void DamageTaken(int damage){
        currentHealth -= damage;   
    }

    public void OnDeath(){
        sesData.Points += pointWorth;
        QueueFree();
    }
}
