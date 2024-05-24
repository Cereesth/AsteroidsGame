using Godot;
using System;

public partial class LaserBoundary : Area2D
{
	[Export] public CollisionShape2D TotalBoundaryArea { get; private set; }
}
