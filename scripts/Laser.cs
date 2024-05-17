using Godot;

public partial class Laser : Area2D
{
    [Export] private float Speed { get; set; } = 300;

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
    }
}
