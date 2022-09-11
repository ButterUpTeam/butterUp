extends Control


func _ready():
	$VBoxContainer/StartButton.grab_focus()
	

func _on_StartButton_pressed():
	var err = get_tree().change_scene("res://scenes/Game.tscn")
	if err: print("Can't change to World scene!")


func _on_ControlsButton_pressed():
	var err = get_tree().change_scene("res://scenes/menus/InfoScreen.tscn")
	if err: print("Can't change to InfoScreen scene!")


func _on_ExitButton_pressed():
	var err = get_tree().change_scene("res://scenes/menus/ExitScreen.tscn")
	if err: print("Can't change to ExitScreen scene!")
