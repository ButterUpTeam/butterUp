extends Node

var TileMapMaskHide = 1


func _ready():
	print("DebuggingTools ready")


func _process(_delta):
	if Input.is_action_just_pressed("db_f1"):
		print("db_f1")
		print("f1 - help\nf2 - nothing\nf3 - mask hide/show")
	elif Input.is_action_just_pressed("db_f2"):
		print("db_f2")
	elif Input.is_action_just_pressed("db_f3"):
		print("db_f3")
		if TileMapMaskHide == 1:
			get_node("/root/World/Game/TileMapMask").hide()
			TileMapMaskHide = 0
			print("mask hide")
		else:
			get_node("/root/World/Game/TileMapMask").show()
			TileMapMaskHide = 1
			print("mask show")
	elif Input.is_action_just_pressed("db_f4"):
		print("db_f4")
	elif Input.is_action_just_pressed("db_f5"):
		print("db_f5")
