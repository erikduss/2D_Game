using Godot;
using System;
using System.Collections.Generic;

public partial class TileRandomTexture : Node
{
	public List<Sprite2D> availableTileTextures = new List<Sprite2D>();
	public int chosenTexture;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (Node sprite in this.GetChildren())
		{
			if(sprite is Sprite2D)
			{
				Sprite2D spriteComponent = sprite.GetNode<Sprite2D>(sprite.GetPath());

                spriteComponent.Visible = false;
				availableTileTextures.Add(spriteComponent);
			}
		}

		chosenTexture = (int)(GD.Randi() % (availableTileTextures.Count-1));

        GD.Print(availableTileTextures.Count + " _ " + chosenTexture);

        availableTileTextures[chosenTexture].Visible = true;
	}
}
