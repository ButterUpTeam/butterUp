extends Node2D

# "0" is multi node, "1" is single node (newer version) 
var butter_version = 1

var butter_scene = load("res://scenes/ButterSpreadParticle.tscn")
var butter_scene_v2 = load("res://scenes/ButterSpreadParticle_v2.tscn")
var butter_instance_v2 = butter_scene_v2.instance()

func _ready():
	butter_instance_v2.emitting = false
	get_node("ViewportContainer/Viewport").add_child(butter_instance_v2)


func _on_GamePlay_Moved(newx, newy):
	if butter_version == 0:
		var butter_instance = butter_scene.instance()
		butter_instance.position = Vector2(newx, newy)
		get_node("ViewportContainer/Viewport").add_child(butter_instance)
	elif butter_version == 1:
		butter_instance_v2.position = Vector2(newx, newy)
		butter_instance_v2.emitting = true
	
