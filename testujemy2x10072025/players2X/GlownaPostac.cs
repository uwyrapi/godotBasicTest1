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
	[Export] public PackedScene BulletScene;
	private Vector2 lastFacingDir = Vector2.Right;
	[Export] public ProgressBar ReloadBar; // drag your bar here in Inspector
	private bool isReloading = false;
	private float reloadTime = 1f; // seconds
	private float reloadTimer = 0f;



	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		velocity.X = 0;

		// --- your existing movement code ---
		if (Input.IsActionPressed("move_left_p1"))
			lastFacingDir = Vector2.Left;
		else if (Input.IsActionPressed("move_right_p1"))
			lastFacingDir = Vector2.Right;
		Vector2 velocity2 = Velocity;
		velocity.X = 0;
		//movement speed relying on torch colletions

		if (OnLadder)
		{
			// No gravity
			velocity.Y = 0;

			// Climbing
			if (Torches == 4 || Torches == 3)
			{
				if (Input.IsActionPressed("move_up_p1"))
					velocity.Y = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_down_p1"))
					velocity.Y = Speed * 1.5f;
				else
					velocity.Y = 0;

				// Horizontal movement
				velocity.X = 0;
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed * 1.5f;
			}
			else
			{
				if (Input.IsActionPressed("move_up_p1"))
					velocity.Y = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_down_p1"))
					velocity.Y = Speed * 1.5f;
				else
					velocity.Y = 0;

				// Horizontal movement 2
				velocity.X = 0;
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed * 1.5f;
			}
		}

		//i set all JumpForce values to be hardcoded because the game crashed after collecting a torch
		switch (Torches)
		{
			case 0:
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed / 4 * 3;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed / 4 * 3;
				JumpForce = 500f;
				break;
			case 1:
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 1.25f;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed * 1.25f;
				JumpForce = 650f;
				//JumpForce = JumpForce * 1.3f;
				break;
			case 2:
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed * 1.5f;
				JumpForce = 650f;
				break;
			case 3:
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 1.5f;
				else if (Input.IsActionPressed("move_right_p1"))
					velocity.X = Speed * 1.5f;
				JumpForce = 650f;
				break;
			case 4:
				if (Input.IsActionPressed("move_left_p1"))
					velocity.X = -Speed * 2f;
				else if (Input.IsActionPressed("move_right_p1"))
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
			GD.Print("Player1 has: " + Torches + " Torches.");
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

		if (!isReloading && Input.IsKeyPressed(Key.Q) && BulletScene != null)
		{
			var bullet = BulletScene.Instantiate<BulletP1cs>();

			float offsetDist = 60f;
			Vector2 spawnOffset = lastFacingDir * offsetDist;

			bullet.GlobalPosition = GlobalPosition + spawnOffset;
			bullet.Init(lastFacingDir);
			GetParent().AddChild(bullet);

			// Start reload
			isReloading = true;
			reloadTimer = 0f;
			if (ReloadBar != null)
				ReloadBar.Value = 0;
		}

		else if (Input.IsKeyPressed(Key.Q) && isReloading)
		{
			//GD.Print("");
			//dont use this unless you're testing something
		}

		//new clip new clip!
		if (isReloading)
		{
			reloadTimer += (float)delta;
			ReloadBar.Value = reloadTimer / reloadTime; // 0 → 1
			if (reloadTimer >= reloadTime)
			{
				isReloading = false;
				ReloadBar.Value = 1;
				//ReloadBar.Visible = false; // ukryj po zakończeniu
			}
		}


		Velocity = velocity;
		MoveAndSlide();
	}
}
