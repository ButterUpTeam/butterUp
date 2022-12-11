extends CanvasLayer


func _ready():
	set_visible(false)
	
# https://www.youtube.com/watch?v=WaotOuDNio8
func _input(event):
	if event.is_action_pressed("pause_button"):
		get_tree().paused = !get_tree().paused # toggle pause mode 
		set_visible(not get_child(0).visible)
		$AudioStreamPlayer.play()

func set_visible(state):
	for child in get_children():
		if "visible" in child:
			child.visible = state
