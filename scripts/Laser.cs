using Godot;

public partial class Laser : Area2D
{
    [Export] private float Speed { get; set; } = 300;

    public override void _Ready()
    {
        this.AreaEntered += Laser_AreaEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right.Rotated(Rotation) * Speed * (float)delta;
    }

    private void Laser_AreaEntered(Area2D area)
    {
        QueueFree();

        if (area is BigAsteroid)
        {
            BigAsteroid asteroid = (BigAsteroid)area;
            asteroid.HealthSystem.TakeDamage(1);
        }
    }
}
