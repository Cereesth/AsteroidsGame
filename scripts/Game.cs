using Godot;
using System;

public partial class Game : Node2D
{
    [Export] private LaserBoundary LaserBoundary;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        LaserBoundary.AreaEntered += LaserBoundary_AreaEntered;
    }

    private void LaserBoundary_AreaEntered(Area2D area)
    {
        if (area is Laser)
        {
            area.QueueFree();
        }
        else if (area is BigAsteroid)
        {
            BigAsteroid asteroid = (BigAsteroid)area;

            //Calculate mirrored position
            float xMid = LaserBoundary.TotalBoundaryArea.GlobalPosition.X;
            float yMid = LaserBoundary.TotalBoundaryArea.GlobalPosition.Y;
            float xReentry = (2 * xMid) - asteroid.GlobalPosition.X;
            float yReentry = (2 * yMid) - asteroid.GlobalPosition.Y;
            float offset = -20;

            //Offset to ensure when it teleports it is not touching the boundary
            if (xReentry < xMid)
            {
                xReentry += offset + (asteroid.ColliderSize.X / 2);
            }
            else
            {
                xReentry -= offset + (asteroid.ColliderSize.X / 2);
            }

            if (yReentry < yMid)
            {
                yReentry += offset + (asteroid.ColliderSize.Y / 2);
            }
            else
            {
                yReentry -= offset + (asteroid.ColliderSize.Y / 2);
            }

            //Set global position
            area.GlobalPosition = new Vector2(xReentry, yReentry);
        }
    }
}
