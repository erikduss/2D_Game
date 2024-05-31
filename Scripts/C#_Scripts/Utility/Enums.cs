using Godot;
using System;

namespace Erikduss
{
    public static class Enums
    {
        public enum PlayerGlobalSettingHeader
        {
            MOVEMENT,
            BASE_ATTACK,
            BASE_DEFENCE,
            STATS
        }

        public enum GrassWorldGeneratorSettingHeader
        {
            PREFAB_PATHS,
            GENERAL_TILE_INFO,
            INITIAL_SPAWN_GENERATION,
            TERRAIN_HEIGHT_SETTINGS,
            TERRAIN_SIZE_SETTINGS
        }
    }
}
