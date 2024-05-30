extends CharacterBody2D


const SPEED = 200.0
const JUMP_VELOCITY = -400.0

var playerSprite: AnimatedSprite2D;
var isMoving: bool = false;
var isInTheAir: bool = false;

# Get the gravity from the project settings to be synced with RigidBody nodes.
var gravity = ProjectSettings.get_setting("physics/2d/default_gravity")

func _ready():
	playerSprite = get_node("AnimatedSprite2D") as AnimatedSprite2D

func _physics_process(delta):
	# Add the gravity.
	if not is_on_floor():
		isInTheAir = true
		velocity.y += gravity * delta
	else:
		isInTheAir = false

	# Handle jump.
	if Input.is_action_just_pressed("Jump") and is_on_floor():
		velocity.y = JUMP_VELOCITY

	# Get the input direction and handle the movement/deceleration.
	var direction = Input.get_axis("Left", "Right")
	
	if direction:
		# Flip the sprite if needed.
		if direction > 0 && playerSprite.flip_h: 
			playerSprite.flip_h = false
		elif direction < 0 && !playerSprite.flip_h: 
			playerSprite.flip_h = true
		
		# Set the animation of walking.
		if !isMoving && !isInTheAir:
			isMoving = true
			playerSprite.play("Walking")
		elif isMoving && isInTheAir:
			isMoving = false
			playerSprite.play("Idle")
		
		velocity.x = direction * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		
		# Set the animation back to Idle.
		if isMoving:
			isMoving = false
			playerSprite.play("Idle")

	move_and_slide()
