extends Area2D

onready var next_lvl = int(get_tree().current_scene.filename) + 1
onready var next_lvl_path = "res://scenes/levels/Level_" + str(next_lvl) + ".tscn"

func _ready():
	pass

func _on_WinToast_body_entered(_body):
	$Sprite.hide()
	$Timer.start()
	$AudioStreamPlayer.play()

func _on_Timer_timeout():
	print(next_lvl_path)
	if next_lvl == 3:
		var err = get_tree().change_scene("res://scenes/menus/ExitScreen.tscn")
		if err: print("Couldn't change to ExitScreen scene")
	else:
		var err = get_tree().change_scene(next_lvl_path)
		if err: print("Couldn't change to Level_" + next_lvl)
