using Godot;
using System;

public partial class ArcherTower : StaticBody2D
{
    // Nodes
	PackedScene baseTile = GD.Load<PackedScene>("res://Scenes/Grid/BaseTile.tscn");
    PackedScene projectile = GD.Load<PackedScene>("res://Scenes/Buildings/ArcherTower/Projectile/Projectile.tscn");
    
    //Locals
    Node2D AttackTarget;
    Godot.Collections.Array<Node2D> targetsInRange = new Godot.Collections.Array<Node2D>();
    Timer shootDelay;


    public int BuildingHealth {get; set;} = 3000;
	private const int BuildingHealthMax = 3000;

    public override void _Ready()
	{
		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();
        AttackTarget = new Node2D{
            GlobalPosition = new Godot.Vector2(5000,200)
        };
        shootDelay =  GetNode<Timer>("shootDelay");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if( BuildingHealth <= 0){ OnDeath(); }
        // Closest enemy will always be the main target
        if(targetsInRange.Count != 0)
        { 
                AttackTarget = targetsInRange[0];
                GetNode<Sprite2D>("Tower").LookAt(AttackTarget.GlobalPosition);
                if(shootDelay.IsStopped())
                {
                    ShootClosestEnemy();
                    shootDelay.Start();
                }
        }
	}

    // If in range
     public void OnEnteredAttackRange(Node2D body){
        if(body.IsInGroup("Enemy")){
            targetsInRange.Add(body);
        }
    }

    public void OnExitedAttackRange(Node2D body){
        targetsInRange.Remove(body);
    }


    // Shoot Enemy
    public void ShootClosestEnemy(){
        Projectile temp = (Projectile)projectile.Instantiate();
        temp.Shoot(AttackTarget.GlobalPosition, this.GlobalPosition);
        GetNode("Projectiles").AddChild(temp);
    }

    // Utlities
	public void DamageTaken(int damage){
		BuildingHealth -= damage;
		GetNode<Label>("HP").Text = BuildingHealth.ToString() + " / " + BuildingHealthMax.ToString();
	}
	private void OnDeath(){
		BaseTile replacement = (BaseTile)baseTile.Instantiate();
		replacement.GlobalPosition = GlobalPosition;
		GetParent().AddChild(replacement);
		QueueFree();
	}

    // Avoiding Deferred state.
}
