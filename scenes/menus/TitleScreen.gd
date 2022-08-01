extends Control


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	$VSplitContainer/AnimationPlayer.play("TitleAnim")
	$VSplitContainer/AnimationPlayer2/Menu.set_position(Vector2(0, 90))
	$AnimationPlayerRect.play("ColorAnim")

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_AnimationPlayer_animation_finished(anim_name):
	if anim_name == "TitleAnim":
		$VSplitContainer/AnimationPlayer.play("TitleUp")
		$VSplitContainer/AnimationPlayer2.play("MenuUp")
	#elif anim_name == "TitleUp":
	#	get_tree().change_scene("res://scenes/menus/Menu.tscn")
