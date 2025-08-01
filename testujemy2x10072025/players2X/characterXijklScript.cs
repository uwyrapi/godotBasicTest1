using Godot;
using System;
using System.Threading;

public partial class characterXijklScript : CharacterBody2D
{
	[Export] public float ijklSpeed = 200f;
	[Export] public float ijklGravity = 1000f;
	[Export] public float ijklJumpForce = 500f;
	[Export] public float ijklTorches = 0f;
	[Export] public bool ijklTorchesDisplay = false;
	[Export] public bool ijklOnLadder = false;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.X = 0;
		//movement speed relying on torch colletions

		if (ijklOnLadder)
		{
			// No gravity
			velocity.Y = 0;

			// Climbing
			if (ijklTorches == 4 || ijklTorches == 3)
			{
				if (Input.IsActionPressed("move_up_p2"))
					velocity.Y = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_down_p2"))
					velocity.Y = ijklSpeed * 1.5f;
				else
					velocity.Y = 0;

				// Horizontal movement
				velocity.X = 0;
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 1.5f;
			}
			else
			{
				if (Input.IsActionPressed("move_up_p2"))
					velocity.Y = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_down_p2"))
					velocity.Y = ijklSpeed * 1.5f;
				else
					velocity.Y = 0;

				// Horizontal movement 2
				velocity.X = 0;
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 1.5f;
			}
		}


		//i set all JumpForce values to be hardcoded because the game crashed after collecting a torch
		switch (ijklTorches)
		{
			case 0:
				//if (Input.IsActionPressed("move_up_p2"))
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed / 4 * 3;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed / 4 * 3;
				ijklJumpForce = 500f;
				break;
			case 1:
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 1.25f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 1.25f;
				ijklJumpForce = 650f;
				//JumpForce = JumpForce * 1.3f;
				break;
			case 2:
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 1.5f;
				ijklJumpForce = 650f;
				break;
			case 3:
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 1.5f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 1.5f;
				ijklJumpForce = 650f;
				break;
			case 4:
				if (Input.IsActionPressed("move_left_p2"))
					velocity.X = -ijklSpeed * 2f;
				else if (Input.IsActionPressed("move_right_p2"))
					velocity.X = ijklSpeed * 2f;
				ijklJumpForce = 650f;
				break;
		}


		// Horizontal movement
		velocity.X = 0;
		if (Input.IsActionPressed("move_left_p2"))
			velocity.X = -ijklSpeed;
		else if (Input.IsActionPressed("move_right_p2"))
			velocity.X = ijklSpeed;


		// displays on torch pickup
		if (ijklTorchesDisplay == false)
		{
			GD.Print("Player2 has: " + ijklTorches + " Torches.");
			ijklTorchesDisplay = true;
		}

		// Gravity
		if (!IsOnFloor())
		{
			velocity.Y += ijklGravity * (float)delta;
		}
		else
			velocity.Y = 0; // reset vertical velocity when grounded

		// Jump
		if (Input.IsActionPressed("move_up_p2") && IsOnFloor())
		{
			velocity.Y = -ijklJumpForce;
		}

		Velocity = velocity;
		MoveAndSlide();

	}
}
