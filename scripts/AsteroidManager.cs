using Godot;
using System;
using System.Collections.Generic;

public partial class AsteroidManager : Node
{
    [Export] private PackedScene BigAsteroid;
    [Export] private PackedScene MediumAsteroid;
    [Export] private PackedScene SmallAsteroid;
    [Export] private PackedScene TinyAsteroid;
    [Export] private Timer SpawnTimer;
    [Export] private AsteroidSpawnArea AsteroidSpawnArea;
    [Export] private Marker2D PlayAreaOrigin;
    [Export] private float BigAsteroidChance = 40;
    [Export] private float MediumAsteroidChance = 30;
    [Export] private float SmallAsteroidChance = 20;
    [Export] private float TinyAsteroidChance = 10;

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
        else if (randomPercent <= TinyAsteroidChance + SmallAsteroidChance + MediumAsteroidChance + BigAsteroidChance)
        {
            SpawnAsteroid(TinyAsteroid.Instantiate<Asteroid>());
        }
        else
        {
            throw new Exception();
        }
    }

    private void SpawnAsteroid(in Asteroid asteroid)
    {
        asteroid.GlobalPosition = GetRandomPoint();

        asteroid.SetDirection(GetRandomDirectionTowardsTarget(asteroid));

        AddChild(asteroid);

        asteroid.OnAsteroidDestroyed += Asteroid_OnAsteroidDestroyed;
    }

    private void Asteroid_OnAsteroidDestroyed(Asteroid asteroid)
    {
        //Unsubscribe from current asteroid event
        asteroid.OnAsteroidDestroyed -= Asteroid_OnAsteroidDestroyed;

        switch (asteroid._AsteroidType)
        {
            case Asteroid.AsteroidType.Big:
                SpawnAsteroidsOnAsteroidDestroyed(GetAsteroidListOnBigAsteroidDestroyed(), asteroid);
                break;
            case Asteroid.AsteroidType.Medium:
                SpawnAsteroidsOnAsteroidDestroyed(GetAsteroidListOnMediumAsteroidDestroyed(), asteroid);
                break;
            case Asteroid.AsteroidType.Small:
                SpawnAsteroidsOnAsteroidDestroyed(GetAsteroidListOnSmallAsteroidDestroyed(), asteroid);
                break;
            case Asteroid.AsteroidType.Tiny:
            default:
                //No new asteroid is spawned
                return;
        }
    }

    private void SpawnAsteroidsOnAsteroidDestroyed(List<Asteroid> asteroidsToSpawn, Asteroid oldAsteroid)
    {
        foreach (Asteroid asteroid in asteroidsToSpawn)
        {
            asteroid.GlobalPosition = oldAsteroid.GlobalPosition;
            asteroid.SetDirection(GetRandomDirection());

            asteroid.OnAsteroidDestroyed += Asteroid_OnAsteroidDestroyed;

            CallDeferred(Node.MethodName.AddChild, asteroid);
        }
    }

    private List<Asteroid> GetAsteroidListOnBigAsteroidDestroyed()
    {
        return new List<Asteroid>
        {
            MediumAsteroid.Instantiate<Asteroid>(),
            MediumAsteroid.Instantiate<Asteroid>()
        };
    }

    private List<Asteroid> GetAsteroidListOnMediumAsteroidDestroyed()
    {
        return new List<Asteroid>
        {
            SmallAsteroid.Instantiate<Asteroid>(),
            SmallAsteroid.Instantiate<Asteroid>(),
            SmallAsteroid.Instantiate<Asteroid>(),
            SmallAsteroid.Instantiate<Asteroid>()
        };
    }

    private List<Asteroid> GetAsteroidListOnSmallAsteroidDestroyed()
    {
        return new List<Asteroid>
        {
            TinyAsteroid.Instantiate<Asteroid>(),
            TinyAsteroid.Instantiate<Asteroid>(),
            TinyAsteroid.Instantiate<Asteroid>()
        };
    }

    private Vector2 GetRandomDirectionTowardsTarget(in Asteroid asteroid)
    {
        Vector2 directionToOrigin = asteroid.Position.DirectionTo(PlayAreaOrigin.GlobalPosition);
        Vector2 directionMinus = directionToOrigin.Rotated(Mathf.DegToRad(-45));
        Vector2 directionPlus = directionToOrigin.Rotated(Mathf.DegToRad(45));
        Vector2 randomDirection = new Vector2((float)GD.RandRange(directionMinus.X, directionPlus.X), (float)GD.RandRange(directionMinus.Y, directionPlus.Y)).Normalized();
        return randomDirection;
    }

    private Vector2 GetRandomDirection()
    {
        Vector2 randomDirection = new Vector2((float)GD.RandRange(-1.0, 1.0), (float)GD.RandRange(-1.0, 1.0)).Normalized();
        return randomDirection;
    }
}
