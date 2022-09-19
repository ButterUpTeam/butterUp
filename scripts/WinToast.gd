extends Area2D

onready var next_lvl = int(get_tree().current_scene.filename) + 1
onready var next_lvl_path = "res://scenes/levels/Level_" + str(next_lvl) + ".tscn"

func _ready():
	pass

func _on_WinToast_body_entered(_body):
	if $Sprite.is_visible():
		$AudioStreamPlayer.play()
		$Timer.start()
	$Sprite.hide()

func _on_Timer_timeout():
	print(next_lvl_path)
	if next_lvl == 3:
		SceneTransition.change_scene("res://scenes/menus/ExitScreen.tscn")	
	else:
		SceneTransition.change_scene(next_lvl_path)
