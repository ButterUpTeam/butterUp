extends Node

var texture: Texture = preload("res://assets/bars.png")

func _ready() -> void:
	var rid: RID = texture.get_rid()
	VisualServer.black_bars_set_images(rid, rid, rid, rid)

	# Note that a Texture (like any Resource) will be unloaded when there
	# will be no more references to it. In such case RIDs to it will no
	# longer be valid. Thus make sure these textures won't be unloaded.
	
	# https://www.reddit.com/r/godot/comments/wbgk7g/is_there_anyway_to_replace_the_black_borders_with/
