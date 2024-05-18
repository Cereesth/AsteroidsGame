using Godot;

public partial class TargetReticle : Node2D
{
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Position = GetGlobalMousePosition();
    }
}