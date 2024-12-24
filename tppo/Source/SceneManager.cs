using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class SceneManager : Node
{
	public static SceneManager Instance {get;set;}
	public static Player Player {get;set;}
	public static Node CurrentScene {get;set;}	
		
	public override void _Ready(){
		base._Ready();
		Instance = this;				
		Player = (Player)Loader.LoadPlayer();
		LoadMain();
	}
	
	public static void LoadMain(){
		ChangeScene(LevelGenerator.GenerateLevel());		
		Player.Position = GetStartingPosition();
		CurrentScene.AddChild(Player);
		//var path = "res://Scenes/Levels/main.tscn";	// If needed for main scene
		//ChangeScene(path);			
	}

	public static void ChangeScene(String path){		
		var scene = ResourceLoader.Load<PackedScene>(path).Instantiate();
		CurrentScene = scene;
		Instance.AddChild(scene);
	}

	public static void ChangeScene(Node scene){				
		CurrentScene = scene;
		Instance.AddChild(scene);
	}
	
	public static Vector2 GetStartingPosition(){
		Vector2 startingPosition = new Vector2(0,0);
		foreach(var node in CurrentScene.GetChildren()){
			if (node.Name == "StartingPosition"){
				startingPosition = ((Node2D)node).Position;				
				break;
			}
		}						
		return startingPosition;
	}

	private static void RemoveEverythingExceptPlayer(){		
		foreach(var node in CurrentScene.GetChildren()){			
			node.QueueFree();							
		}								
	}

	public static void ChangeRoom(String nextRoom){		
		CurrentScene.RemoveChild(Player);		
		RemoveEverythingExceptPlayer();
		ChangeScene(LevelGenerator.GenerateLevel());		
		Player.Position = GetStartingPosition();	
		CurrentScene.AddChild(Player);					
		// TODO: Maybe need to refactor LevelGenerator a bit 
		// ChangeScene(nextRoom);	
		//ChangeScene(LevelGenerator.GenerateLevel(nextRoom));			
	}
}
