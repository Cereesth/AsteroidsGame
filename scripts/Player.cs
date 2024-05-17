using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	private float Speed = 300.0f;

	[Export]
	private Node2D Target;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 newVelocity = Velocity;
		Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");

		if (direction != Vector2.Zero)
		{
			newVelocity.X = direction.X * Speed * Convert.ToSingle(delta);
			newVelocity.Y = direction.Y * Speed * Convert.ToSingle(delta);
		}
		else
		{
			newVelocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			newVelocity.Y = Mathf.MoveToward(newVelocity.Y, 0, Speed);
		}

		//Handle Rotations
		LookAt(Target.Position);

		Velocity = newVelocity;
		MoveAndSlide();
	}
}
