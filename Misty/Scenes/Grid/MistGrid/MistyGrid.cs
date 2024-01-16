using Godot;
using System;

public partial class MistyGrid : StaticBody2D
{
    game_settings gmst;

    public override void _Ready()
    {
        gmst = GetNode<game_settings>("/root/GameSettings");
        gmst.monsterSpawns.Add(GetNode<Node2D>("MonsterSpawner").GlobalPosition);
    }
}
