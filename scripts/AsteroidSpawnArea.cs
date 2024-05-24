using Godot;
using System;
using Godot.Collections;

public partial class AsteroidSpawnArea : Area2D
{
    [Export] public Array<CollisionShape2D> SpawnAreas;
}
