using Godot;
using System;

public partial class BulletP1cs : Node2D // or whatever the root node is
{
	public Vector2 Direction = Vector2.Zero;

	public void Init(Vector2 direction)
	{
		Direction = direction;
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Direction * 600f * (float)delta;
	}
}
