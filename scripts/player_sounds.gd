extends Node

var slide_sound_playing = false

func _ready():
	pass # Replace with function body.


func _on_Player_Jumped():
	$jump_2.play()


func _on_Player_Moved(_newx, _newy, player_motion):
	if !slide_sound_playing:
		slide_sound_playing = true
		$sliding.play()
	if !$sliding.playing:
		slide_sound_playing = false
		
	if player_motion.length() < 10:
		$sliding.stop()
		
	$sliding.pitch_scale = clamp((player_motion.abs().x * 0.1), 0.1, 2.0)
	$sliding.volume_db = clamp((player_motion.abs().x * 0.2 - 20), -20, 1.0)
