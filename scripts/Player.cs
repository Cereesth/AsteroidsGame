using Godot;

public partial class Player : CharacterBody2D
{

    #region Properties
    [Export] private float Speed { get; set; } = 300.0f;
    [Export] private Node2D Target { get; set; }
    [Export] private PackedScene LaserScene { get; set; }
    [Export] private Marker2D ShootPoint { get; set; }
    [Export] private Timer ShootTimer { get; set; }
    #endregion

    #region Methods
    public override void _Ready()
    {
        ShootTimer.Timeout += ShootTimer_Timeout;
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
    }

    private void HandleMovement(in double delta)
    {
        Vector2 newVelocity;
        Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

        if (direction != Vector2.Zero)
        {
            newVelocity = direction * Speed * (float)delta;
        }
        else
        {
            newVelocity = new Vector2(Mathf.MoveToward(Velocity.X, 0, Speed), Mathf.MoveToward(Velocity.Y, 0, Speed));
        }

        //Handle Rotations
        LookAt(Target.Position);

        Velocity = newVelocity;
        MoveAndSlide();
    }
    #endregion

    #region Events
    private void ShootTimer_Timeout()
    {
        Laser laser = LaserScene.Instantiate<Laser>();

        laser.Rotation = ShootPoint.GlobalRotation;
        laser.Position = ShootPoint.GlobalPosition;

        ShootPoint.AddChild(laser);
    }
    #endregion

}
