using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class GameState : Node
{
	// TODO: 
	// could leave it like this or somehovv connect it to actual Player class
	// or rename them so one handles the movement and other one handles stats
	// but enemy has it's ovvn class so it seems inconsistent here
	public static Player player{get{return SceneManager.Player;}}	
}
