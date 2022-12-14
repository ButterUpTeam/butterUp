extends Node2D


var butter_scene_v2 = preload("res://scenes/ButterSpreadParticle_v2.tscn")
var butter_instance_v2 = butter_scene_v2.instance()

func spread_some_butter(x, y):
	butter_instance_v2.position = Vector2(x, y)
	butter_instance_v2.emitting = true
	butter_instance_v2.get_node("Particles2D_front").emitting = true
	butter_instance_v2.get_node("Particles2D_base").emitting = true

func _ready():
	butter_instance_v2.emitting = false
	get_node("ViewportContainer/Viewport").add_child(butter_instance_v2)
	
func _on_Player_Moved(newx, newy):
	spread_some_butter(newx, newy)
	
func _on_Player_JustFalled(newx, newy):
	print("Player Falled")
	spread_some_butter(newx, newy)
