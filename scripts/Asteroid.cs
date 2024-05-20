using Godot;

public partial class Asteroid : Area2D
{
    [Export] private float MinSpeed { get; set; } = 5;
    [Export] private float MaxSpeed { get; set; } = 300;
    [Export] private float MaxAbsoluteRotateSpeed { get; set; } = 2;
    [Export] private CollisionShape2D ColliderShape { get; set; }
    [Export] public HealthSystem HealthSystem { get; private set; }

    private float RotateSpeed { get; set; }
    private float Speed { get; set; }
    private Vector2 Direction { get; set; }
    public Vector2 ColliderSize { get; private set; }

    public override void _Ready()
    {
        RotateSpeed = (float)GD.RandRange(-MaxAbsoluteRotateSpeed, MaxAbsoluteRotateSpeed);
        Speed = (float)GD.RandRange(MinSpeed, MaxSpeed);
        ColliderSize = ColliderShape.Shape.GetRect().Size;

        HealthSystem.OnDeath += HealthSystem_OnDeath;
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Direction * Speed * (float)delta;
        Rotation += RotateSpeed * (float)delta;
    }

    public void SetDirection(in Vector2 direction)
    {
        Direction = direction;
    }

    private void HealthSystem_OnDeath()
    {
        QueueFree();
    }
}
