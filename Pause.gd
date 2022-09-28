extends CanvasLayer


func _ready():
	$PauseToast.visible = false
	$ColorRect.visible = false
	
# https://www.youtube.com/watch?v=WaotOuDNio8
func _input(event):
	if event.is_action_pressed("pause_button"):
		get_tree().paused = !get_tree().paused # toggle pause mode
		$PauseToast.visible = !$PauseToast.visible
		$ColorRect.visible = !$ColorRect.visible
