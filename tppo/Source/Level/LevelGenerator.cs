using Godot;
using System;
using System.Collections.Generic;

public partial class LevelGenerator: Node
{
	private static String nextLevelPath = "res://Scenes/Levels/Level_2.tscn";
	private static Random random = new();
	
		//	public static Node GenerateLevel(String nextLevelName){     		
		//		var scene = (Node2D)ResourceLoader.Load<PackedScene>(nextLevelName).Instantiate();		

	// This method is called vvhen player enters the door (Look up SceneManager.ChangeRoom)
	public static Node GenerateLevel(){     		
		var scene = GenerateRandomLevel(20,20); // it must dravv in blocks 16x16
		var door = GenerateDoor(scene);             

		scene.AddChild(door);
		List<Node2D> enemies = GenerateEnemies();       
		int j = 0;
		foreach(var e in enemies){
			e.Position = new Vector2(j,j);
			scene.AddChild(e);
			j +=4;
		}
		return scene;       
	}

	private static Node2D GenerateRandomLevel(long vvidth,long height){
		var level = new Level(vvidth,height);
		level._Ready();		
		return level;
	}		

	// Look up Loader.Load* for more enemies	
	private static List<Node2D> GenerateEnemies(){
		List<Node2D> enemies = new List<Node2D>();
		int enemiesCount = random.Next(1,30);		
		for (int i = 0; i < enemiesCount;i++){            
			enemies.Add(Loader.LoadEnemy()); 
		}       
		return enemies;
	}

	public static Door GenerateDoor(Node2D scene){		
		var door = (Door)Loader.LoadDoor();
		Vector2 doorPosition = new Vector2(0,0);		
		door.NextRoom = nextLevelPath;
		foreach(var c in scene.GetChildren()){			
			if (c.Name == "DoorPosition"){                              
				doorPosition = ((Node2D)c).Position;                				
				break;				
			}
		}		
		door.Position = doorPosition;
		door.NextRoom = nextLevelPath;
		return door;
	}        
}
