using Godot;
using System;

public partial class Torch2 : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node body)
	{
			if (body is GlownaPostac player1)
			{
				GD.Print("Player 1 picked up a torch!");
				player1.Torches += 1;
				player1.TorchesDisplay = false;
				QueueFree();
			}
			else if (body is characterXijklScript player2)
			{
				GD.Print("Player 2 picked up a torch!");
				player2.ijklTorches += 1;
				player2.ijklTorchesDisplay = false;
				QueueFree();
			}
	}
}
