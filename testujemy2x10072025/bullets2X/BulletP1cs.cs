using Godot;
using System;

public partial class BulletP1cs : Area2D
{
	[Export] public float Speed = 400f;
	private Vector2 _direction = new();

	public void Init(Vector2 dir)
	{
		_direction = dir.Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += _direction * Speed * (float)delta;
		if (!GetViewportRect().HasPoint(Position))
			QueueFree();
	}

}
