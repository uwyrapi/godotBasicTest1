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
		if (body is GlownaPostac player)
		{
			player.OnLadder = true;
		}
	}

	private void OnBodyExited(Node body)
	{
		if (body is GlownaPostac player)
		{
			player.OnLadder = false;
		}
	}
}
