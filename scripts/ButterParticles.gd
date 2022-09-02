extends Particles2D

const MAXIMUM_NUMBER_OF_PARTICLES = 150


func _ready():
	self.emitting = true
	self.one_shot = true
	self.lifetime = rand_range(5, 25)
	get_node("ButterParticlesLifetime").wait_time = rand_range(10, 35)


func _on_ButterParticlesLifetime_timeout():
	var n_particles = get_parent().get_children().size()
	
	if n_particles <= MAXIMUM_NUMBER_OF_PARTICLES:
		get_node("ButterParticlesLifetime").start(rand_range(5, 15))
	else:
		queue_free()
