using Godot;
using System;

public partial class PlayerMove : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public AnimatedSprite2D playerSpriteAnimator;
	private bool isMoving = false;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Ready()
	{
		base._Ready();
		
		playerSpriteAnimator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			velocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("Left", "Right", "ui_up", "ui_down");
		
		/*if(direction.x > 0){
			//we need to face right
			if(playerSpriteAnimator.flip_h) playerSpriteAnimator.flip_h = false;
		}
		else if(direction.x < 0){
			//we need to face left
			if(!playerSpriteAnimator.flip_h) playerSpriteAnimator.flip_h = true;
		}
		else{
			//we dont do anything
		}*/
		
		if (direction != Vector2.Zero)
		{
			if(!isMoving) 
			{
				isMoving = true;
				//playerSpriteAnimator.play("Walking");
			}
			
			velocity.X = direction.X * Speed;
			GD.Print("Moving " + direction);
		}
		else
		{
			if(isMoving) 
			{
				isMoving = false;
				//playerSpriteAnimator.play("Idle");
			}
			
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
