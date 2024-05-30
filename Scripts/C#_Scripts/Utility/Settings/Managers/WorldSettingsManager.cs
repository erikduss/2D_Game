using Godot;
using System;

namespace Erikduss
{
	public partial class WorldSettingsManager : Node2D
	{
		public static WorldSettingsManager Instance;

		public PlayerGlobalSettingsManager playerGlobalSettingsManager = new PlayerGlobalSettingsManager();

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				QueueFree();
			}

			playerGlobalSettingsManager.LoadGlobalPlayerSettings();
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void SetValuesOfPlayerGlobalSettings(PlayerGlobalSettingsConfig config)
		{
            PlayerManager.Instance.playerLocomotion.movementSpeed = config.movementSpeed;
			PlayerManager.Instance.playerLocomotion.jumpVelocity = -config.jumpVelocity;
		}
	}
}
