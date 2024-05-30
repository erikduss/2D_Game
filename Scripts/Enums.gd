extends Node

#Player save value generalized headers.
enum PlayerGlobalSettingHeader {MOVEMENT, BASE_ATTACK, BASE_DEFENCE, STATS}

func GetEnumString(enumDirectory, enumValue) -> String:
	return enumDirectory.keys()[enumValue]
