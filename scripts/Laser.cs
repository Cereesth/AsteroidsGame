using Godot;

public partial class Laser : Area2D
{
    [Export] private float Speed { get; set; } = 300;
    public Area2D Boundary { get; set; }

    public override void _Ready()
    {
        //Boundary.AreaExited += Boundary_AreaExited;
    }

    //private void Boundary_AreaExited(Area2D area)
    //{
    //    QueueFree();
    //}

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
    }
}
