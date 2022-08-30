extends Node2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"
# https://godotengine.org/qa/20915/godot-3-load-preload-functions-in-c%23
var butter_scene = load("res://scenes/ButterSpreadParticle.tscn")


# Called when the node enters the scene tree for the first time.
func _ready():
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
#	pass


func _on_GamePlay_Moved(newx, newy):
	var butter_instance = butter_scene.instance()
	butter_instance.position = Vector2(newx, newy)
	add_child(butter_instance)
