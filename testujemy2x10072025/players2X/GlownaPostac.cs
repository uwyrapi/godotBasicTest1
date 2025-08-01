using Godot;
using System;
using System.Threading;


public partial class GlownaPostac : CharacterBody2D
{
	[Export] public float Speed = 200f;
	[Export] public float Gravity = 1000f;
	[Export] public float JumpForce = 500f;
	[Export] public float Torches = 0f;
	[Export] public bool TorchesDisplay = false;
	[Export] public bool OnLadder = false;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.X = 0;
		//movement speed relying on torch colletions
		
		if (OnLadder)
		{
			// No gravity
			velocity.Y = 0;

			// Climbing
			if (Torches == 4 || Torches == 3)
			{
				if (Input.IsKeyPressed(Key.W))
					velocity.Y = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.S))
					velocity.Y = Speed * 1.5f;
				else
					velocity.Y = 0;
				
				// Horizontal movement
					velocity.X = 0;
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 1.5f;
			}
			else
			{
				if (Input.IsKeyPressed(Key.W))
					velocity.Y = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.S))
					velocity.Y = Speed * 1.5f;
				else
					velocity.Y = 0;
				
				// Horizontal movement 2
					velocity.X = 0;
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 1.5f;
			}
		}
		
		//i set all JumpForce values to be hardcoded because the game crashed after collecting a torch
		switch (Torches)
		{
			case 0:
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed / 4 * 3;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed / 4 * 3;
				JumpForce = 500f;
				break;
			case 1:
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 1.25f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 1.25f;
				JumpForce = 650f;
				//JumpForce = JumpForce * 1.3f;
				break;
			case 2:
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 1.5f;
				JumpForce = 650f;
				break;
			case 3:
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 1.5f;
				JumpForce = 650f;
				break;
			case 4:
				if (Input.IsKeyPressed(Key.A))
					velocity.X = -Speed * 2f;
				else if (Input.IsKeyPressed(Key.D))
					velocity.X = Speed * 2f;
				JumpForce = 650f;
				break;

		}

		/*
		// Horizontal movement
		velocity.X = 0;
		if (Input.IsKeyPressed(Key.A))
			velocity.X = -Speed;
		else if (Input.IsKeyPressed(Key.D))
			velocity.X = Speed;
		*/

		// displays on torch pickup
		if (TorchesDisplay == false)
		{
			GD.Print("You have: " + Torches + " Torches.");
			TorchesDisplay = true;
		}

		// Gravity
		if (!IsOnFloor())
		{
			velocity.Y += Gravity * (float)delta;
		}
		else
			velocity.Y = 0; // reset vertical velocity when grounded

		// Jump
		if (Input.IsActionPressed("jump_p1") && IsOnFloor())
		{
			velocity.Y = -JumpForce;
		}

		//instructions on how the bullet goes
		if (Input.IsKeyPressed(Key.Q))
		{
			var bullet = GD.Load<PackedScene>("res://Bullet.tscn").Instantiate<BulletP1cs>();
			GetParent().AddChild(bullet);
			bullet.GlobalPosition = GlobalPosition;
			bullet.Init(Input.IsKeyPressed(Key.A) ? Vector2.Left :
						Input.IsKeyPressed(Key.D) ? Vector2.Right : Vector2.Up);
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
