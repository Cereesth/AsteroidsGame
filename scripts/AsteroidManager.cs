using Godot;
using System;
using System.Runtime.InteropServices;

public partial class AsteroidManager : Node
{
    [Export] private PackedScene BigAsteroid;
    [Export] private PackedScene MediumAsteroid;
    [Export] private PackedScene SmallAsteroid;
    [Export] private PackedScene TinyAsteroid;
    [Export] private Timer SpawnTimer;
    [Export] private AsteroidSpawnArea AsteroidSpawnArea;
    [Export] private Marker2D PlayAreaOrigin;
    [Export] private float BigAsteroidChance = 50;
    [Export] private float MediumAsteroidChance = 30;
    [Export] private float SmallAsteroidChance = 20;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
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
    }

    private void SpawnTimer_Timeout()
    {
        float randomPercent = (float)GD.RandRange(1.0, 100.0);

        //TODO: There's got to be a better way to do this
        if (randomPercent <= BigAsteroidChance)
        {
            SpawnAsteroid(BigAsteroid.Instantiate<Asteroid>());
        }
        else if (randomPercent <= MediumAsteroidChance + BigAsteroidChance)
        {
            SpawnAsteroid(MediumAsteroid.Instantiate<Asteroid>());
        }
        else if (randomPercent <= SmallAsteroidChance + MediumAsteroidChance + BigAsteroidChance)
        {
            SpawnAsteroid(SmallAsteroid.Instantiate<Asteroid>());
        }
        else
        {
            throw new Exception();
        }
    }

    private void SpawnAsteroid(in Asteroid asteroid)
    {
        asteroid.GlobalPosition = GetRandomPoint();

        asteroid.SetDirection(GetRandomDirection(asteroid));

        AddChild(asteroid);
    }

    private Vector2 GetRandomDirection(in Asteroid asteroid)
    {
        Vector2 directionToOrigin = asteroid.Position.DirectionTo(PlayAreaOrigin.GlobalPosition);
        Vector2 directionMinus = directionToOrigin.Rotated(Mathf.DegToRad(-45));
        Vector2 directionPlus = directionToOrigin.Rotated(Mathf.DegToRad(45));
        Vector2 randomDirection = new Vector2((float)GD.RandRange(directionMinus.X, directionPlus.X), (float)GD.RandRange(directionMinus.Y, directionPlus.Y)).Normalized();
        return randomDirection;
    }
}
