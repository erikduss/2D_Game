using Godot;
using System;

namespace Erikduss
{
	public partial class PlayerLocomotion : CharacterBody2D
	{
		public float movementSpeed = 200.0f;
		public float jumpVelocity = -400.0f;

		public AnimatedSprite2D playerSpriteAnimator;
		private bool isMoving = false;
        private bool isInTheAir = false;

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
			{
				isInTheAir = true;
                
                velocity.Y += gravity * (float)delta;
            }
			else
			{
				if (isInTheAir)
				{
                    isInTheAir = false;
                    playerSpriteAnimator.Play("Idle");
                }
			}

			// Handle Jump.
			if (Input.IsActionJustPressed("Jump") && IsOnFloor())
			{
                velocity.Y = jumpVelocity;
                playerSpriteAnimator.Play("Falling");
            }
				

			// Get the input direction and handle the movement/deceleration.
			float direction = Input.GetAxis("Left", "Right");

			if(direction > 0)
			{
				//we need to face right
				if(playerSpriteAnimator.FlipH) 
					playerSpriteAnimator.FlipH = false;
			}
			else if(direction < 0)
			{
				//we need to face left
				if(!playerSpriteAnimator.FlipH) 
					playerSpriteAnimator.FlipH = true;
			}
			else
			{
				//we dont do anything
			}

			if (direction != 0)
			{
				if (!isMoving && !isInTheAir)
				{
					isMoving = true;
					playerSpriteAnimator.Play("Walking");
				}
				else if (isMoving && isInTheAir)
				{
                    isMoving = false;
                    playerSpriteAnimator.Play("Falling");
                }

				velocity.X = direction * movementSpeed;
			}
			else
			{
				if (isMoving)
				{
					isMoving = false;
					playerSpriteAnimator.Play("Idle");
				}

				velocity.X = Mathf.MoveToward(Velocity.X, 0, movementSpeed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}
	}
}
