extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	$VBoxContainer/StartButton.grab_focus()
	var tree = get_tree()
	


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_StartButton_pressed():
	get_tree().change_scene("res://scenes/World.tscn")


func _on_ControlsButton_pressed():
	get_tree().change_scene("res://scenes/menus/InfoScreen.tscn")


func _on_ExitButton_pressed():
	get_tree().change_scene("res://scenes/menus/ExitScreen.tscn")
