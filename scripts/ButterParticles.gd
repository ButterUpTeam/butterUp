extends Particles2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	self.emitting = true


func _on_ButterParticlesLifetime_timeout():
	queue_free()
