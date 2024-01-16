using Godot;
using System;
using System.Linq;

public partial class GameManager : Node
{
    PackedScene zombie = GD.Load<PackedScene>("res://Scenes/Enemies/Zombie/Zombie.tscn");
    game_settings gmst;
    session_data sndata;

    Timer luminiteGatherDelay;
    public override void _Ready()
    {
        // Autoloads
        gmst = GetNode<game_settings>("/root/GameSettings");
        sndata = GetNode<session_data>("/root/SessionData");

        // Signals
        gmst.WaveStart += SpawnMonsters;

        // Children
        luminiteGatherDelay = GetNode<Timer>("luminiteGatherDelay");
    }

    public override void _Process(double delta)
    {
        if(luminiteGatherDelay.IsStopped()){
            sndata.CurrentLuminiteCrystals += sndata.LuminiteRate;
            luminiteGatherDelay.Start();
        }
    }

    public void OnGameOver(){

    }

    public void SpawnMonsters(int monstersCount){
        for(int i = 0; i < monstersCount; i++){
            Random rnd = new Random();
            Zombie monster = (Zombie)zombie.Instantiate();
            monster.GlobalPosition = gmst.monsterSpawns.ElementAt(rnd.Next(0, gmst.monsterSpawns.Count));
            GetNode("/root/Main/Enemies").AddChild(monster);
        }
    }
}
