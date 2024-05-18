using Godot;
using System;

public partial class BigAsteroid : Area2D
{
    [Export] private float Speed { get; set; } = 300;
    [Export] private float MaxAbsoluteRotateSpeed { get; set; } = 2;

    private float RotateSpeed { get; set; }

    public override void _Ready()
    {
        RotateSpeed = (float)GD.RandRange(-MaxAbsoluteRotateSpeed, MaxAbsoluteRotateSpeed);
    }

    public override void _PhysicsProcess(double delta)
    {
        //Position += Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
        Rotation += RotateSpeed * (float)delta;
    }
}
