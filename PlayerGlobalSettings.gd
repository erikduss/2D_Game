extends BaseSettings

func _ready():
	LoadGlobalPlayerSettings()

func LoadGlobalPlayerSettings():
	var score_data = {}
	var config = ConfigFile.new()

	# Load data from a file.
	var err = config.load(fullFilePath)

	# If the file didn't load, ignore it.
	if err != OK:
		CreateNewSaveFile(true)

	# Iterate over all sections.
	for player in config.get_sections():
		# Fetch the data for each section.
		var player_name = config.get_value(player, "player_name")
		var player_score = config.get_value(player, "best_score")
		score_data[player_name] = player_score

func CreateNewSaveFile(overrideSaveFile):
	var config = ConfigFile.new()

	# Load data from a file.
	var err = config.load(fullFilePath)

	# If the file loaded, we dont want to override it.
	if err == OK && !overrideSaveFile:
		return
		
	#Check if the directory is defined.
	if(directoryLocation.is_empty()): directoryLocation = "user://Settings//Player//"
	
	#Check if the directory exists, otherwise create the directory.
	if DirAccess.dir_exists_absolute(directoryLocation):
		print("Directory exists!")
	else:
		var error_code = DirAccess.make_dir_recursive_absolute(directoryLocation)
	
	# Store some values.
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.MOVEMENT), "movementSpeed", 200)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.MOVEMENT), "jumpVelocity", 200)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.BASE_ATTACK), "attackDamageMultiplier", 1)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.BASE_DEFENCE), "defenceMultiplier", 1)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.STATS), "startingHealth", 100)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.STATS), "startingStamina", 100)
	config.set_value(Enums.GetEnumString(Enums.PlayerGlobalSettingHeader, Enums.PlayerGlobalSettingHeader.STATS), "startingMana", 100)

	# Save it to a file.
	config.save(fullFilePath)
