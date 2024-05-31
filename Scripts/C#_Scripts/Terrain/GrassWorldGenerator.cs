using Godot;
using System;

public partial class GrassWorldGenerator : Node
{
	public PackedScene grassGroundTilePrefab;
	public PackedScene grassGroundLeftCornerTilePrefab;
	public PackedScene grassGroundRightCornerTilePrefab;

	//General Tile info
	public int tileSize = 64;
	public Vector2 tileStartingPosition = new Vector2(0,60);

	//Tiles going to the left from the spawn.
	public int amountOfTilesGoingLeftFromSpawn = 3;
	public int minAmountOfTilesGoingLeft = 1;
	public int maxAmountOfTimesGoingLeft = 10;

	//Overall Generation
	public int chanceToChangeHeightPerTile;
	public int minChanceToChangeHeightPerTile = 5;
	public int maxChanceToChangeHeightPerTile = 15;

	public int chanceToGoDownInsteadOfUp = 50;
	public int minChanceToGoDownInsteadOfUp = 50;
	public int maxChanceToGoDownInsteadOfUp = 50;

	public int maxAmoutOfTilesUp = 100;	//height of the world limit (value * 64 - tile start position.y is the actual world position)
	public int maxAmountOfTilesDown = 100; //depth of the world limit

	//Length of the world. (Does not include tiles going left from spawn)
	public int worldTileLength;
	public int maxAmountOfWorldTileLength = 1000;
	public int minAmountOfWorldTileLength = 250;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Load the prefabs of the tiles.
		grassGroundTilePrefab = GD.Load<PackedScene>("res://Prefabs//Ground_Tile.tscn");
        grassGroundLeftCornerTilePrefab = GD.Load<PackedScene>("res://Prefabs//Ground_Tile_Left_Corner.tscn");
        grassGroundRightCornerTilePrefab = GD.Load<PackedScene>("res://Prefabs//Ground_Tile_Right_Corner.tscn");

		//This should be done when values are loaded form file.
		GenerateRandomValuesForGeneration();

        GenerateWorld();
    }

	public void GenerateRandomValuesForGeneration()
	{
		//We check for equal to 0 value to prevent errors with dividing by 0.
		if(maxAmountOfTimesGoingLeft - minAmountOfTilesGoingLeft == 0)
		{
			amountOfTilesGoingLeftFromSpawn = minAmountOfTilesGoingLeft;
        }
		else
		{
            amountOfTilesGoingLeftFromSpawn = (int)(GD.Randi() % (maxAmountOfTimesGoingLeft - minAmountOfTilesGoingLeft) + minAmountOfTilesGoingLeft);
        }
        GD.Print("Random: " + amountOfTilesGoingLeftFromSpawn);

		if(maxAmountOfWorldTileLength - minAmountOfWorldTileLength == 0)
		{
			worldTileLength = minAmountOfWorldTileLength;
        }
		else
		{
            worldTileLength = (int)(GD.Randi() % (maxAmountOfWorldTileLength - minAmountOfWorldTileLength) + minAmountOfWorldTileLength);
        }
        GD.Print("Random world length: " + worldTileLength);

		if (maxChanceToChangeHeightPerTile - minChanceToChangeHeightPerTile == 0)
		{
			chanceToChangeHeightPerTile = minChanceToChangeHeightPerTile;
        }
		else
		{
            chanceToChangeHeightPerTile = (int)(GD.Randi() % (maxChanceToChangeHeightPerTile - minChanceToChangeHeightPerTile) + minChanceToChangeHeightPerTile);
        }
		GD.Print("Chance to change height: " + chanceToChangeHeightPerTile);

		if(maxChanceToGoDownInsteadOfUp - minChanceToGoDownInsteadOfUp == 0)
		{
			chanceToGoDownInsteadOfUp = minChanceToGoDownInsteadOfUp;
        }
		else
		{
            chanceToGoDownInsteadOfUp = (int)(GD.Randi() % (maxChanceToGoDownInsteadOfUp - minChanceToGoDownInsteadOfUp) + minChanceToGoDownInsteadOfUp);
        }
    }

	public void GenerateWorld()
	{
		GenerateGround();
    }

	public void GenerateGround()
	{
		float lastTileYValue = tileStartingPosition.Y;
		bool lastTileWasHeightChange = false;

		//Spawning all the tiles.
		for(int i = 0; i < worldTileLength; i++)
		{
			//roll a randomizer to choose if we change height or go straight.
			int changeHeightThisTile = (int)(GD.Randi() % 100);
			//We can only change height if we pass the randomizer value check and if the last tile wasnt a height change.
			if(changeHeightThisTile < chanceToChangeHeightPerTile && !lastTileWasHeightChange)
			{
				lastTileWasHeightChange = true;
                //we change height.
                int doWeGoDownInstead = (int)(GD.Randi() % 100);
				if(doWeGoDownInstead < chanceToGoDownInsteadOfUp)
				{
                    //We go down -> using right corner
                    Node2D tileInstance = (Node2D)grassGroundRightCornerTilePrefab.Instantiate();
                    tileInstance.GlobalPosition = new Vector2(i * tileSize, lastTileYValue);
                    //we adjust the tileYValue variable.
                    lastTileYValue += tileSize;
                    tileInstance.Name = "Right_Corner_Down_Tile_" + i;
                    AddChild(tileInstance);
                }
				else
				{
                    //We go up -> using left corner
                    Node2D tileInstance = (Node2D)grassGroundLeftCornerTilePrefab.Instantiate();
                    //we adjust the tileYValue variable, we do this after because the next tile should be affected. Not this one.
                    lastTileYValue -= tileSize;
                    tileInstance.GlobalPosition = new Vector2(i * tileSize, lastTileYValue);
                    tileInstance.Name = "Left_Corner_Up_Tile_" + i;
                    AddChild(tileInstance);
                }
            }
			else //we go straight
			{
				lastTileWasHeightChange = false;
                Node2D tileInstance = (Node2D)grassGroundTilePrefab.Instantiate();
                tileInstance.GlobalPosition = new Vector2(i * tileSize, lastTileYValue);
                tileInstance.Name = "Tile_" + i;
                AddChild(tileInstance);
            }
		}

		//Spawning to the left
		for(int i = 0; i < amountOfTilesGoingLeftFromSpawn; i++)
		{
            Node2D tileInstance = (Node2D)grassGroundTilePrefab.Instantiate();
            tileInstance.GlobalPosition = new Vector2((i+1) * -tileSize, tileStartingPosition.Y);
            tileInstance.Name = "Tile_Back_" + i;
            AddChild(tileInstance);
        }
	}
}
