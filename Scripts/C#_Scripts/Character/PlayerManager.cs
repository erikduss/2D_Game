using Erikduss;
using Godot;
using System;

public partial class PlayerManager : Node2D
{
	public static PlayerManager Instance { get; private set; }

	public PlayerLocomotion playerLocomotion;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			QueueFree();
		}

		playerLocomotion = GetNode<PlayerLocomotion>("CharacterBody2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}


}
