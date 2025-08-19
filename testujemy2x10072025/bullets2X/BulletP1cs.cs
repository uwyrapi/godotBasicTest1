using Godot;
using System;

public partial class BulletP1cs : Area2D
{
	public Vector2 Direction = Vector2.Zero;
	[Export] public float Speed = 1200f;
	private Timer _lifeTimer;

	public void Init(Vector2 direction)
	{
		Direction = direction.Normalized();
	}

	public override void _PhysicsProcess(double delta)
	{
		Position += Direction * Speed * (float)delta;
	}

	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		// Create a timer that will kill this node after a few seconds
		_lifeTimer = new Timer();
		_lifeTimer.WaitTime = 3f;
		_lifeTimer.OneShot = true;
		_lifeTimer.Timeout += () => QueueFree();
		AddChild(_lifeTimer);
		_lifeTimer.Start();
	}
	private void OnBodyEntered(Node body)
	{
		if (body is GlownaPostac player1)
		{
			GD.Print("Player 1 got hit!");
			//player1.hp += 1;
			//player1.hpDisplay = false;
			QueueFree();
		}
		else if (body is characterXijklScript player2)
		{
			GD.Print("Player 2 got hit!");
			//player2.hp = player1.hp - 1;
			//player2.ijklhpDisplay = false;
			QueueFree();
		}
		else if (body is TileMap) // map condition
		{
			GD.Print("Hit the TileMap!");
			QueueFree();
		}
		/*
		else if (body is MagicWall1 || body is MagicWall2 || body is MagicWall3 || body is MagicWall4) // extra condition
		{
			GD.Print("Hit something?");
			QueueFree();
		}
		*/
	}
}
