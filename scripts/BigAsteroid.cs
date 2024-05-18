using Godot;
using System;

public partial class BigAsteroid : Area2D
{
    [Export] private float MaxSpeed { get; set; } = 300;
    [Export] private float MaxAbsoluteRotateSpeed { get; set; } = 2;
    [Export] private CollisionShape2D ColliderShape { get; set; }

    private float RotateSpeed { get; set; }
    private Vector2 RandomDirection { get; set; }
    public Vector2 ColliderSize { get; private set; }

    public override void _Ready()
    {
        RotateSpeed = (float)GD.RandRange(-MaxAbsoluteRotateSpeed, MaxAbsoluteRotateSpeed);
        RandomDirection = new Vector2((float)GD.RandRange(-MaxSpeed, MaxSpeed), (float)GD.RandRange(-MaxSpeed, MaxSpeed));
        ColliderSize = ColliderShape.Shape.GetRect().Size;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += RandomDirection * (float)delta;
        Rotation += RotateSpeed * (float)delta;
    }
}
