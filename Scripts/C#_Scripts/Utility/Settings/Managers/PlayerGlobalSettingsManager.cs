using Godot;
using System;

namespace Erikduss
{
	public partial class PlayerGlobalSettingsManager : BaseSettingsManager
	{
		public PlayerGlobalSettingsConfig settingsConfig = new PlayerGlobalSettingsConfig();

		private void SetFileInfo()
		{
			directoryLocation = "user://Settings//Player//";
			fileName = "PlayerGlobalSettings.cfg";
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

		public virtual void LoadGlobalPlayerSettings()
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
				if(section == Enums.PlayerGlobalSettingHeader.MOVEMENT.ToString())
				{
                    settingsConfig.movementSpeed = (float)config.GetValue(section, "movementSpeed");
                    settingsConfig.jumpVelocity = (float)config.GetValue(section, "jumpVelocity");
                }
				else if (section == Enums.PlayerGlobalSettingHeader.BASE_ATTACK.ToString())
				{
                    settingsConfig.attackDamageMultiplier = (float)config.GetValue(section, "attackDamageMultiplier");
                }
				else if (section == Enums.PlayerGlobalSettingHeader.BASE_DEFENCE.ToString())
				{
                    settingsConfig.defenceMultiplier = (float)config.GetValue(section, "defenceMultiplier");
                }
				else if (section == Enums.PlayerGlobalSettingHeader.STATS.ToString())
				{
                    settingsConfig.startingHealth = (int)config.GetValue(section, "startingHealth");
                    settingsConfig.startingStamina = (int)config.GetValue(section, "startingStamina");
                    settingsConfig.startingMana = (int)config.GetValue(section, "startingMana");
                }
				else
				{
					GD.Print("SECTION NOT FOUND: " + section);
				}
			}

			WorldSettingsManager.Instance.SetValuesOfPlayerGlobalSettings(settingsConfig);
        }

		public override void CreateNewSaveFile(bool overrideSaveFile)
		{
			base.CreateNewSaveFile(overrideSaveFile);

			// Store some values.

			config.SetValue(Enums.PlayerGlobalSettingHeader.MOVEMENT.ToString(), "movementSpeed", settingsConfig.movementSpeed);
			config.SetValue(Enums.PlayerGlobalSettingHeader.MOVEMENT.ToString(), "jumpVelocity", settingsConfig.jumpVelocity);
			config.SetValue(Enums.PlayerGlobalSettingHeader.BASE_ATTACK.ToString(), "attackDamageMultiplier", settingsConfig.attackDamageMultiplier);
			config.SetValue(Enums.PlayerGlobalSettingHeader.BASE_DEFENCE.ToString(), "defenceMultiplier", settingsConfig.defenceMultiplier);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingHealth", settingsConfig.startingHealth);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingStamina", settingsConfig.startingStamina);
			config.SetValue(Enums.PlayerGlobalSettingHeader.STATS.ToString(), "startingMana", settingsConfig.startingMana);

			// Save it to a file.
			config.Save(fullFilePath);
		}
	}
}
