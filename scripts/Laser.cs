using Godot;

public partial class Laser : Area2D
{
    [Export] private float Speed { get; set; } = 300;

    private Vector2 Direction { get; set; }

    public override void _Ready()
    {
        Direction = Vector2.Right.Rotated(Rotation);

        this.AreaEntered += Laser_AreaEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction * Speed * (float)delta;
    }

    private void Laser_AreaEntered(Area2D area)
    {
        QueueFree();

        if (area is Asteroid)
        {
            Asteroid asteroid = (Asteroid)area;
            asteroid.HealthSystem.TakeDamage(1);
        }
    }
}
