using Godot;
using System;

namespace Erikduss
{
	public partial class GrassWorldGeneratorSettingsConfig : Node
	{
        //Prefab Paths
        public string grassGroundTilePrefabPath = "res://Prefabs//Ground_Tile.tscn";
        public string grassGroundLeftCornerTilePrefabPath = "res://Prefabs//Ground_Tile_Left_Corner.tscn";
        public string grassGroundRightCornerTilePrefabPath = "res://Prefabs//Ground_Tile_Right_Corner.tscn";
        public string grassGroundDirtTilePrefabPath = "res://Prefabs//Ground_Dirt_Tile.tscn";

        public string groundLeftLineOverridePrefabPath = "res://Prefabs//Ground_Dirt_LeftLineOverride.tscn";
        public string groundRightLineOverridePrefabPath = "res://Prefabs//Ground_Dirt_RightLineOverride.tscn";

        //General Tile info
        public int tileSize = 64;
        public Vector2 tileStartingPosition = new Vector2(0, 60);

        //Initial Spawn Generation
        public int minAmountOfTilesGoingLeft = 1;
        public int maxAmountOfTimesGoingLeft = 10;

        //Terrain Height Settings
        public int minChanceToChangeHeightPerTile = 5;
        public int maxChanceToChangeHeightPerTile = 15;

        public int minChanceToGoDownInsteadOfUp = 50;
        public int maxChanceToGoDownInsteadOfUp = 50;

        public int maxAmoutOfTilesUp = 100; 
        public int maxAmountOfTilesDown = 100;

        //Terrain Size Settings
        public int maxAmountOfWorldTileLength = 1000;
        public int minAmountOfWorldTileLength = 250;

        public int minAmountOfDirtTilesPerTile = 10;
        public int maxAmountOfDirtTilesPerTile = 20;
    }
}
