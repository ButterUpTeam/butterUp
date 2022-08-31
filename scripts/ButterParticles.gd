extends Particles2D


# Declare member variables here. Examples:
# var a = 2
# var b = "text"


# Called when the node enters the scene tree for the first time.
func _ready():
	self.emitting = true
	self.one_shot = true
	self.lifetime = rand_range(5, 25)
	get_node("ButterParticlesLifetime").wait_time = rand_range(10, 35)


func _on_ButterParticlesLifetime_timeout():
	var n_particles = get_parent().get_children().size()
	
	if n_particles <= 150:
		get_node("ButterParticlesLifetime").start(rand_range(5, 15))
	else:
		queue_free()
