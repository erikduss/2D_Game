using Godot;
using System;
using System.Reflection.Emit;

namespace Erikduss
{
	public partial class DialogManager : Node
	{
        public static DialogManager Instance { get; private set; }

        [Export] public Godot.Label dialogTypeLabel;
        [Export] public Godot.Label dialogTextLabel;

        [Export] public Godot.CanvasItem mainDialogNode;

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

            if(mainDialogNode == null) mainDialogNode = GetNode("Dialog_Panel_Parent") as Godot.CanvasItem;

            mainDialogNode.Hide();
        }

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

        public void ShowDialog(Enums.DialogType dialogType, string dialog)
        {
            if (dialogType == Enums.DialogType.GAME_MESSAGE)
            {
                dialogTypeLabel.Text = "Game Message";
            }
            else
            {
                dialogTypeLabel.Text = "Dialog";
            }
            
            dialogTextLabel.Text = dialog;
            mainDialogNode.Show();
        }

        public void HideDialog()
        {
            mainDialogNode.Hide();
        }
	}
}
