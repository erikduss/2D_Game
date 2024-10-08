using Erikduss;
using Godot;
using System;

namespace Erikduss
{
	public partial class PlayerManager : CharacterManager
	{
		public static PlayerManager Instance { get; private set; }

		public PlayerLocomotion playerLocomotion;

		public bool isLoaded = false;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();

			if (Instance == null)
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
}
