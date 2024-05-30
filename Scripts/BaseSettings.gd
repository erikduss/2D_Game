extends Node

var directoryLocation: String = "user://Settings//Player//"
var fileName: String = "PlayerGlobalSettings.cfg"
var fullFilePath: String

var directoryAccessor: DirAccess

# Called when the node enters the scene tree for the first time.
func _ready():
	fullFilePath = directoryLocation + fileName


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	pass
	
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
		
