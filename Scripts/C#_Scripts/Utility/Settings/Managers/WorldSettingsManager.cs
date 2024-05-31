using Godot;
using System;
using System.Threading.Tasks;

namespace Erikduss
{
	public partial class WorldSettingsManager : Node2D
	{
		public static WorldSettingsManager Instance { get; private set; }

		public PlayerGlobalSettingsManager playerGlobalSettingsManager = new PlayerGlobalSettingsManager();
        public GrassWorldGeneratorSettingsManager grassWorldGeneratorSettingsManager = new GrassWorldGeneratorSettingsManager();

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

            //grassWorldGeneratorSettingsManager._Ready();

            InitializeWorld();

            
            
        }

        async void InitializeWorld()
        {
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
            grassWorldGeneratorSettingsManager.LoadGrassWorldSettings();
            await ToSignal(GetTree().CreateTimer(0.01f), "timeout");
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

            PlayerManager.Instance.isLoaded = true;
        }

		public void SetValuesOfGrassWorldGeneratorAsync(GrassWorldGeneratorSettingsConfig config)
		{
            if (config == null) GD.Print("Config is null");
            if (GrassWorldGenerator.Instance == null) GD.Print("Grass World Generator is null");

            GrassWorldGenerator.Instance.grassGroundTilePrefabPath = config.grassGroundTilePrefabPath;
            GrassWorldGenerator.Instance.grassGroundLeftCornerTilePrefabPath = config.grassGroundLeftCornerTilePrefabPath;
            GrassWorldGenerator.Instance.grassGroundRightCornerTilePrefabPath = config.grassGroundRightCornerTilePrefabPath;
            GrassWorldGenerator.Instance.grassGroundDirtTilePrefabPath = config.grassGroundDirtTilePrefabPath;

            GrassWorldGenerator.Instance.groundLeftLineOverridePrefabPath = config.groundLeftLineOverridePrefabPath;
            GrassWorldGenerator.Instance.groundRightLineOverridePrefabPath = config.groundRightLineOverridePrefabPath; ;

            GrassWorldGenerator.Instance.tileSize = config.tileSize;
            GrassWorldGenerator.Instance.tileStartingPosition = config.tileStartingPosition;

            GrassWorldGenerator.Instance.minAmountOfTilesGoingLeft = config.minAmountOfTilesGoingLeft;
            GrassWorldGenerator.Instance.maxAmountOfTimesGoingLeft = config.maxAmountOfTimesGoingLeft;

            GrassWorldGenerator.Instance.minChanceToChangeHeightPerTile = config.minChanceToChangeHeightPerTile;
            GrassWorldGenerator.Instance.maxChanceToChangeHeightPerTile = config.maxChanceToChangeHeightPerTile;

            GrassWorldGenerator.Instance.minChanceToGoDownInsteadOfUp = config.minChanceToGoDownInsteadOfUp;
            GrassWorldGenerator.Instance.maxChanceToGoDownInsteadOfUp = config.maxChanceToGoDownInsteadOfUp;

            GrassWorldGenerator.Instance.maxAmoutOfTilesUp = config.maxAmoutOfTilesUp;
            GrassWorldGenerator.Instance.maxAmountOfTilesDown = config.maxAmountOfTilesDown;

            GrassWorldGenerator.Instance.maxAmountOfWorldTileLength = config.maxAmountOfWorldTileLength;
            GrassWorldGenerator.Instance.minAmountOfWorldTileLength = config.minAmountOfWorldTileLength;

            GrassWorldGenerator.Instance.minAmountOfDirtTilesPerTile = config.minAmountOfDirtTilesPerTile;
            GrassWorldGenerator.Instance.maxAmountOfDirtTilesPerTile = config.maxAmountOfDirtTilesPerTile;


            GrassWorldGenerator.Instance.InitializeAndGenerateWorld();
		}
	}
}
