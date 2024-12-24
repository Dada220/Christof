using Godot;
using System;

public partial class MainMenu : Control
{
	private void _on_StartGame_pressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/SceneManager.tscn");
	}
		private void _on_ExitGame_pressed()
	{
		GetTree().Quit();
	}
}
