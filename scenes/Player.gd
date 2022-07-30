extends KinematicBody2D

const MAXSPEED = 100
const GRAVITY = 10
const ACCEL = 30
const JUMPFORCE = 70
const MAXFALLSPEED = 300

enum {DIR_RIGHT, DIR_LEFT, DIR_DOWN, DIR_UP}

class jump:
	enum {acceleration, slowing_down, idle}

var motion = Vector2()


class Player:
	var direction = Vector2(DIR_RIGHT, DIR_DOWN)
	var jump_phase = jump.idle

var player = Player.new()

func _ready():
	pass

func _physics_process(_delta):

	if Input.is_action_pressed("mv_left"):
		motion.x -=ACCEL
		player.direction.x = DIR_LEFT
	elif Input.is_action_pressed("mv_right"):
		motion.x += ACCEL
		player.direction.x = DIR_RIGHT
	else:
		motion.x = lerp(motion.x, 0, 0.2)

	motion.x = clamp(motion.x, -MAXSPEED, MAXSPEED)

	if Input.is_action_pressed("mv_up") and is_on_floor():
		player.direction.y = DIR_UP
		player.jump_phase = jump.acceleration
		motion.y = 0

	if player.direction.y == DIR_UP:
		if motion.y > -JUMPFORCE and player.jump_phase == jump.acceleration:
			motion.y = lerp(motion.y, -JUMPFORCE-5, 0.1)
		elif motion.y < 0:
			motion.y = lerp(motion.y, 10, 0.1)
			player.jump_phase = jump.slowing_down
		else:
			player.direction.y = DIR_DOWN
			motion.y = 0
			player.jump_phase = jump.idle
	else:
		motion.y += GRAVITY
		motion.y = clamp(motion.y, 0, MAXFALLSPEED)

	if player.direction.x == DIR_RIGHT:
		$Sprite.scale.x = 1
	else:
		$Sprite.scale.x = -1

	motion = move_and_slide(motion, Vector2.UP)
