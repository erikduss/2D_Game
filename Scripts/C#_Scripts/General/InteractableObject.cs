using Godot;
using System;

namespace Erikduss
{
	public partial class InteractableObject : Node2D, IInteractable
	{
		[Export] public Enums.DialogType dialogType = Enums.DialogType.GAME_MESSAGE;
		[Export] public string interactionText = "Default Dialog";

		public bool canCurrentlyInteract = false;
		public bool hasBeenInterectedWith = false;

		public Godot.Node parentNode;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready()
		{
			parentNode = this.GetParent();
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
			if (!canCurrentlyInteract || hasBeenInterectedWith) return;

            if (Input.IsKeyPressed(Key.E))
            {
				Interact();
            }
        }

		public void Interact()
		{
            GD.Print("INPUT PRESSED");

			hasBeenInterectedWith = true;

            InGameUIManager.Instance.HideDialog();
            canCurrentlyInteract = false;

            parentNode.QueueFree();
        }

		public void OnArea2DAreaEntered(Node2D body)
		{
			if (hasBeenInterectedWith) return;

			InGameUIManager.Instance.ShowDialog(dialogType, interactionText);
			canCurrentlyInteract = true;
        }

        public void OnArea2DAreaExited(Node2D body)
        {
			InGameUIManager.Instance.HideDialog();
            canCurrentlyInteract = false;
        }
    }
}
