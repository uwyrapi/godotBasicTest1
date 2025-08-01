using Godot;
using System;

public partial class Ladder1 : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	private void OnBodyEntered(Node body)
	{
		if (body is GlownaPostac player1)
		{
			player1.OnLadder = true;
		}
		if (body is characterXijklScript player2)
		{
			player2.ijklOnLadder = true;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is GlownaPostac player1)
		{
			player1.OnLadder = false;
		}
		if (body is characterXijklScript player2)
		{
			player2.ijklOnLadder = false;
		}
	}
}
