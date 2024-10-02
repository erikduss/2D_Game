using Godot;
using System;

namespace Erikduss
{
	public partial class InGameUIManager : Node
	{
        public static InGameUIManager Instance { get; private set; }

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
		{
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                QueueFree();
            }
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{

		}

        public void ShowDialog(Enums.DialogType dialogType, string dialog)
        {
            DialogManager.Instance.ShowDialog(dialogType, dialog);
        }

        public void HideDialog()
        {
            DialogManager.Instance.HideDialog();
        }
	}
}
