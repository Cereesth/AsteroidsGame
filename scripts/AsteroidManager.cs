using Godot;
using System;

public partial class AsteroidManager : Node
{
    [Export] private PackedScene BigAsteroid;
    [Export] private PackedScene MediumAsteroid;
    [Export] private PackedScene SmallAsteroid;
    [Export] private PackedScene TinyAsteroid;
    [Export] private Timer SpawnTimer;
    [Export] private AsteroidSpawnArea AsteroidSpawnArea;

    private Vector2 ScreenSize;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ScreenSize = GetViewport().GetVisibleRect().Size;
        SpawnTimer.Timeout += SpawnTimer_Timeout;
    }

    private Vector2 GetRandomPoint()
    {
        int randomAreaNum = GD.RandRange(0, AsteroidSpawnArea.SpawnAreas.Count - 1);
        CollisionShape2D randomArea = AsteroidSpawnArea.SpawnAreas[randomAreaNum];
        Vector2 randomAreaSize = randomArea.Shape.GetRect().Size;
        float randomX = (float)GD.RandRange(randomArea.GlobalPosition.X - (randomAreaSize.X / 2), randomArea.GlobalPosition.X + (randomAreaSize.X / 2));
        float randomY = (float)GD.RandRange(randomArea.GlobalPosition.Y - (randomAreaSize.Y / 2), randomArea.GlobalPosition.Y + (randomAreaSize.Y / 2));
        return new Vector2(randomX, randomY);
        //return new Vector2((float)GD.RandRange(0, ScreenSize.X), (float)GD.RandRange(0, ScreenSize.Y));
    }

    private void SpawnTimer_Timeout()
    {
        BigAsteroid asteroid = BigAsteroid.Instantiate<BigAsteroid>();

        asteroid.Position = GetRandomPoint();

        AddChild(asteroid);
    }
}
