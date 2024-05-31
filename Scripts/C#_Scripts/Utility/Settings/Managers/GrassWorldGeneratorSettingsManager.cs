using Erikduss;
using Godot;
using System;

namespace Erikduss
{
    public partial class GrassWorldGeneratorSettingsManager : BaseSettingsManager
    {
        public GrassWorldGeneratorSettingsConfig settingsConfig = new GrassWorldGeneratorSettingsConfig();

        private void SetFileInfo()
        {
            directoryLocation = "user://Settings//WorldGeneration//";
            fileName = "GrassWorldGeneratorSettings.cfg";
            fullFilePath = directoryLocation + fileName;
        }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
        {
            base._Ready();
        }

        // Called every frame. 'delta' is the elapsed time since the previous frame.
        public override void _Process(double delta)
        {
        }

        public virtual void LoadGrassWorldSettings()
        {
            //We need to ensure the correct file paths are set before we continue.
            SetFileInfo();

            var config = new ConfigFile();

            // Load data from a file.
            var err = config.Load(fullFilePath);

            // If the file didn't load, ignore it.
            if (err != Error.Ok)
                CreateNewSaveFile(true);

            // Iterate over all sections.
            foreach (String section in config.GetSections())
            {
                if (section == Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString())
                {
                    settingsConfig.grassGroundTilePrefabPath = (string)config.GetValue(section, "grassGroundTilePrefabPath");
                    settingsConfig.grassGroundLeftCornerTilePrefabPath = (string)config.GetValue(section, "grassGroundLeftCornerTilePrefabPath");
                    settingsConfig.grassGroundRightCornerTilePrefabPath = (string)config.GetValue(section, "grassGroundRightCornerTilePrefabPath");
                    settingsConfig.grassGroundDirtTilePrefabPath = (string)config.GetValue(section, "grassGroundDirtTilePrefabPath");

                    settingsConfig.groundLeftLineOverridePrefabPath = (string)config.GetValue(section, "groundLeftLineOverridePrefabPath");
                    settingsConfig.groundRightLineOverridePrefabPath = (string)config.GetValue(section, "groundRightLineOverridePrefabPath");
                }
                else if (section == Enums.GrassWorldGeneratorSettingHeader.GENERAL_TILE_INFO.ToString())
                {
                    settingsConfig.tileSize = (int)config.GetValue(section, "tileSize");
                    settingsConfig.tileStartingPosition = (Vector2)config.GetValue(section, "tileStartingPosition");
                }
                else if (section == Enums.GrassWorldGeneratorSettingHeader.INITIAL_SPAWN_GENERATION.ToString())
                {
                    settingsConfig.minAmountOfTilesGoingLeft = (int)config.GetValue(section, "minAmountOfTilesGoingLeft");
                    settingsConfig.maxAmountOfTimesGoingLeft = (int)config.GetValue(section, "maxAmountOfTimesGoingLeft");
                }
                else if (section == Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString())
                {
                    settingsConfig.minChanceToChangeHeightPerTile = (int)config.GetValue(section, "minChanceToChangeHeightPerTile");
                    settingsConfig.maxChanceToChangeHeightPerTile = (int)config.GetValue(section, "maxChanceToChangeHeightPerTile");

                    settingsConfig.minChanceToGoDownInsteadOfUp = (int)config.GetValue(section, "minChanceToGoDownInsteadOfUp");
                    settingsConfig.maxChanceToGoDownInsteadOfUp = (int)config.GetValue(section, "maxChanceToGoDownInsteadOfUp");

                    settingsConfig.maxAmoutOfTilesUp = (int)config.GetValue(section, "maxAmoutOfTilesUp");
                    settingsConfig.maxAmountOfTilesDown = (int)config.GetValue(section, "maxAmountOfTilesDown");
                }
                else if (section == Enums.GrassWorldGeneratorSettingHeader.TERRAIN_SIZE_SETTINGS.ToString())
                {
                    settingsConfig.maxAmountOfWorldTileLength = (int)config.GetValue(section, "maxAmountOfWorldTileLength");
                    settingsConfig.minAmountOfWorldTileLength = (int)config.GetValue(section, "minAmountOfWorldTileLength");

                    settingsConfig.minAmountOfDirtTilesPerTile = (int)config.GetValue(section, "minAmountOfDirtTilesPerTile");
                    settingsConfig.maxAmountOfDirtTilesPerTile = (int)config.GetValue(section, "maxAmountOfDirtTilesPerTile");
                }
                else
                {
                    GD.Print("SECTION NOT FOUND: " + section);
                }
            }

            
            WorldSettingsManager.Instance.SetValuesOfGrassWorldGeneratorAsync(settingsConfig);
        }

        public override void CreateNewSaveFile(bool overrideSaveFile)
        {
            base.CreateNewSaveFile(overrideSaveFile);

            // Store some values.
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "grassGroundTilePrefabPath", settingsConfig.grassGroundTilePrefabPath);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "grassGroundLeftCornerTilePrefabPath", settingsConfig.grassGroundLeftCornerTilePrefabPath);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "grassGroundRightCornerTilePrefabPath", settingsConfig.grassGroundRightCornerTilePrefabPath);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "grassGroundDirtTilePrefabPath", settingsConfig.grassGroundDirtTilePrefabPath);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "groundLeftLineOverridePrefabPath", settingsConfig.groundLeftLineOverridePrefabPath);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.PREFAB_PATHS.ToString(), "groundRightLineOverridePrefabPath", settingsConfig.groundRightLineOverridePrefabPath);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.GENERAL_TILE_INFO.ToString(), "tileSize", settingsConfig.tileSize);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.GENERAL_TILE_INFO.ToString(), "tileStartingPosition", settingsConfig.tileStartingPosition);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.INITIAL_SPAWN_GENERATION.ToString(), "minAmountOfTilesGoingLeft", settingsConfig.minAmountOfTilesGoingLeft);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.INITIAL_SPAWN_GENERATION.ToString(), "maxAmountOfTimesGoingLeft", settingsConfig.maxAmountOfTimesGoingLeft);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "minChanceToChangeHeightPerTile", settingsConfig.minChanceToChangeHeightPerTile);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "maxChanceToChangeHeightPerTile", settingsConfig.maxChanceToChangeHeightPerTile);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "minChanceToGoDownInsteadOfUp", settingsConfig.minChanceToGoDownInsteadOfUp);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "maxChanceToGoDownInsteadOfUp", settingsConfig.maxChanceToGoDownInsteadOfUp);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "maxAmoutOfTilesUp", settingsConfig.maxAmoutOfTilesUp);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_HEIGHT_SETTINGS.ToString(), "maxAmountOfTilesDown", settingsConfig.maxAmountOfTilesDown);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_SIZE_SETTINGS.ToString(), "maxAmountOfWorldTileLength", settingsConfig.maxAmountOfWorldTileLength);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_SIZE_SETTINGS.ToString(), "minAmountOfWorldTileLength", settingsConfig.minAmountOfWorldTileLength);

            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_SIZE_SETTINGS.ToString(), "minAmountOfDirtTilesPerTile", settingsConfig.minAmountOfDirtTilesPerTile);
            config.SetValue(Enums.GrassWorldGeneratorSettingHeader.TERRAIN_SIZE_SETTINGS.ToString(), "maxAmountOfDirtTilesPerTile", settingsConfig.maxAmountOfDirtTilesPerTile);

            // Save it to a file.
            config.Save(fullFilePath);
        }
    }
}
