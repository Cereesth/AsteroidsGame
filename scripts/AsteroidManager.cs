using Godot;
using System;

public partial class AsteroidManager : Node
{
    [Export] private PackedScene BigAsteroid;
    [Export] private PackedScene MediumAsteroid;
    [Export] private PackedScene SmallAsteroid;
    [Export] private PackedScene TinyAsteroid;
    [Export] private Timer SpawnTimer;

    private Vector2 ScreenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewport().GetVisibleRect().Size;
        SpawnTimer.Timeout += SpawnTimer_Timeout;
    }

    private Vector2 GetRandomPoint()
    {
        return new Vector2((float)GD.RandRange(0, ScreenSize.X), (float)GD.RandRange(0, ScreenSize.Y));
    }

    private void SpawnTimer_Timeout()
    {
        BigAsteroid asteroid = BigAsteroid.Instantiate<BigAsteroid>();

        asteroid.Position = GetRandomPoint();

        AddChild(asteroid);
    }
}
