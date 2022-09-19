extends CanvasLayer

# credit to: https://www.youtube.com/watch?v=yZQStB6nHuI
func change_scene(target: String) -> void:
	$AnimationPlayer.play("dissolve")
	yield($AnimationPlayer, "animation_finished")
	var err = get_tree().change_scene(target)
	if err: print("Couldn't change scene to: " + target)
	$AnimationPlayer.play_backwards("dissolve")
