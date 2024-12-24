using Godot;
using System;

public partial class Door : Area2D
{
	private bool Entered {get;set;} = false;
	[Export] public String NextRoom {get;set;}

	public Door(){
		NextRoom = "res://Scenes/Levels/Level_1.tscn"; // DEBUG option 	
		Entered = false;	
	}
	
	public override void _Process(double delta){
		if (Entered){           
			SceneManager.ChangeRoom(NextRoom);						
			if (Input.IsActionJustPressed("Accept")){	// IF DOESN'T VVORK: Add "Accept" action via Project -> Input map 				
			}
		}
	}

	// Added by clicking on Node on right panel and pressing body_entered signal
	void OnBodyEntered(Node2D body) {		
		if (body is Player)
			Entered = true;
	}

	// Added by clicking on Node on right panel and pressing body_exited signal 
	void OnBodyExited(Node2D body){     
		if (body is Player)
			Entered = false;
	}   
}
