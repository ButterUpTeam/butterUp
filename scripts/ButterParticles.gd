extends Particles2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	self.emitting = true
	self.one_shot = true
	self.lifetime = 10
	get_node("ButterParticlesLifetime").wait_time = 15


func _on_ButterParticlesLifetime_timeout():
	queue_free()
