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
	if not File.new().file_exists(next_lvl_path):
		SceneTransition.change_scene("res://scenes/menus/ExitScreen.tscn")	
	else:
		SceneTransition.change_scene(next_lvl_path)
	print(next_lvl_path)
