using Godot;
using System;

public partial class GameOver : Node2D
{
    public override void _Ready()
    {
        GetNode<Camera2D>("Camera2D").MakeCurrent();
    }
}
