extends Control


func _ready():
	$VBoxContainer/StartButton.grab_focus()
	

func _on_StartButton_pressed():
	get_tree().change_scene("res://scenes/World.tscn")


func _on_ControlsButton_pressed():
	get_tree().change_scene("res://scenes/menus/InfoScreen.tscn")


func _on_ExitButton_pressed():
	get_tree().change_scene("res://scenes/menus/ExitScreen.tscn")
