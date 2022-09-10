extends Node

var TileMapMaskHide = 1
var ButterSpreadVersion = 1


func _ready():
	print("DebuggingTools ready")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta):
	if Input.is_action_just_pressed("db_f1"):
		print("db_f1")
		if TileMapMaskHide == 1:
			get_node("/root/World/Game/TileMapMask").hide()
			TileMapMaskHide = 0
		else:
			get_node("/root/World/Game/TileMapMask").show()
			TileMapMaskHide = 1
	elif Input.is_action_just_pressed("db_f2"):
		print("db_f2")
		ButterSpreadVersion = 0 if ButterSpreadVersion == 1 else 1
		get_node("/root/World/Game/ButterSpreadContainer").butter_version = ButterSpreadVersion
	elif Input.is_action_just_pressed("db_f3"):
		print("db_f3")
	elif Input.is_action_just_pressed("db_f4"):
		print("db_f4")
	elif Input.is_action_just_pressed("db_f5"):
		print("db_f5")
