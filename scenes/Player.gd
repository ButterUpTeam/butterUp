extends KinematicBody2D

const UP = Vector2(0,-1)
const MAXSPEED = 100

var motion = Vector2()


func _ready():
	pass
	
func _physics_process(delta):
	
	if Input.is_action_pressed("mv_left"):
		motion.x = -MAXSPEED
	elif Input.is_action_pressed("mv_right"):
		motion.x = MAXSPEED
	else:
		motion.x = 0

	motion = move_and_slide(motion, UP)
