using Godot;
using System;

namespace Erikduss
{
	public partial class TitleScreenManager : Node
	{
		[Export] public string gameLoadingSceneName = "LoadingToGame";

        // Called when the node enters the scene tree for the first time.
        public override void _Ready()
		{
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void StartGame()
		{
			GetTree().ChangeSceneToFile("res://GameScene/" + gameLoadingSceneName + ".tscn");
		}

		public void OpenOptions()
		{

		}

		public void CloseGame()
		{

		}
	}
}
