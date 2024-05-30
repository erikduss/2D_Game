using Godot;
using System;

namespace Erikduss
{
	public partial class PlayerGlobalSettings : BaseSettings
	{
		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			base._Ready();
			LoadGlobalPlayerSettings();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public virtual void LoadGlobalPlayerSettings()
		{
			var score_data = new Godot.Collections.Dictionary();
			var config = new ConfigFile();

			// Load data from a file.
			var err = config.Load(fullFilePath);

			// If the file didn't load, ignore it.
			if (err != Error.Ok)
				CreateNewSaveFile(true);

			// Iterate over all sections.
			foreach (String player in config.GetSections())
			{
				// Fetch the data for each section.
				var player_name = (String)config.GetValue(player, "player_name");
				var player_score = (int)config.GetValue(player, "best_score");
				score_data[player_name] = player_score;
			}
		}

		public override void CreateNewSaveFile(bool overrideSaveFile)
		{
			base.CreateNewSaveFile(overrideSaveFile);

			// Store some values.

			config.SetValue(Enums.PlayerGlobalSettingHeader.MOVEMENT.ToString(), "movementSpeed", 200);
			config.SetValue(Enums.PlayerGlobalSettingHeader.MOVEMENT.ToString(), "jumpVelocity", 200);
			config.SetValue(Enums.PlayerGlobalSettingHeader.BASE_ATTACK.ToString(), "attackDamageMultiplier", 1);
			config.SetValue(Enums.PlayerGlobalSettingHeader.BASE_DEFENCE.ToString(), "defenceMultiplier", 1);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingHealth", 100);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingStamina", 100);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingMana", 100);


			// Save it to a file.
			config.Save(fullFilePath);
		}
	}
}
