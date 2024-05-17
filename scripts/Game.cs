using Godot;
using System;

public partial class Game : Node2D
{
	[Export] private Area2D LaserBoundary;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        //Cache the boundary in the lasers?
        //Laser.Boundary = LaserBoundary;
        LaserBoundary.AreaExited += LaserBoundary_AreaExited;
	}

    private void LaserBoundary_AreaExited(Area2D area)
    {
        area.QueueFree();
    }
}
