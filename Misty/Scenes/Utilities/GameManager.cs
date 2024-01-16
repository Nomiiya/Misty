using Godot;
using System;

public partial class GameManager : Node
{
    PackedScene zombie = GD.Load<PackedScene>("res://Scenes/Enemies/Zombie/Zombie.tscn");
    game_settings gmst;
    public override void _Ready()
    {
        gmst = GetNode<game_settings>("/root/GameSettings");
        // Connect("WaveStart", SpawnMonsters(gmst.monsterSpawns[0], 10))
        gmst.WaveStart += SpawnMonsters(gmst.monsterSpawns[0], 10);
    }

    public void OnGameOver(){

    }

    public void SpawnMonsters(Godot.Vector2 spawnPoint, int monstersCount){
        for(int i = 0; i < monstersCount; i++){
            Zombie monster = (Zombie)zombie.Instantiate();
            monster.GlobalPosition = spawnPoint;
            GetNode("Enemies").AddChild(monster);
        }
    }
}
