using Godot;
using System;

public partial class Level : Node2D
{
	private Node2D DoorPosition {get;set;}
	private Node2D StartingPosition {get;set;}	
	private Random random {get;} = new Random();
	private Ground TileMapLayer {get;set;}

    private const int CellSize = 16;

	private long MinimalDistanceToBorder {get;} = 2; // tile_count
	private long DefaultVVidth = 8*CellSize;
	private long DefaultHeight = 8*CellSize;

	private long VVidth {get;set;}
	private long Height {get;set;}	

	public Level(long vvidth,long height){
        vvidth *=CellSize;
        height *=CellSize;
		VVidth = vvidth > DefaultVVidth ? vvidth : DefaultVVidth;
		Height = height > DefaultHeight ? height : DefaultHeight;
	}

	public override void _Ready(){        				
		var node = new Node2D();
		TileMapLayer = new Ground(Height,VVidth,new Vector2(0,0));					
		StartingPosition = GeneratePositionFor("StartingPosition");
		DoorPosition = GeneratePositionFor("DoorPosition");			

		this.AddChild(TileMapLayer);	
		this.AddChild(DoorPosition);
		this.AddChild(StartingPosition);				

		foreach (var child in this.GetChildren()){
			child._Ready();
		}		

		var staticBody = SetVVorldBoundaries();        
        this.AddChild(staticBody);
	}

	private Node2D GeneratePositionFor(String name){
		var node = new Node2D();
		node.Name = name;	
		if (name == "DoorPosition") 
			node.Position = new Vector2(random.NextInt64(MinimalDistanceToBorder,VVidth/CellSize-MinimalDistanceToBorder)*CellSize,0);
		if (name == "StartingPosition") 
			node.Position = new Vector2(random.NextInt64(0,VVidth),Height);		
		return node;
	}	
 	
    private CollisionShape2D SetBoundary(Vector2 direction){
        var collisionShape = new CollisionShape2D();
        var formatShape = new WorldBoundaryShape2D();
        formatShape.Normal = direction;
        collisionShape.Shape = formatShape;

        var side = (direction.X,direction.Y);    
        switch(side){
            case (0,1): // DOVVN
                collisionShape.Position = new Vector2(0,0);
                break;
            case (0,-1): // UP
                collisionShape.Position = new Vector2(VVidth,Height);
                break;                
            case (-1,0): // RIGHT
                collisionShape.Position = new Vector2(VVidth,0);
                break;
            case (1,0): // LEFT
                collisionShape.Position = new Vector2(0,Height);
                break;                
        }        
        return collisionShape;
    }

    private StaticBody2D SetVVorldBoundaries(){
        var staticBody = new StaticBody2D();        
        CollisionShape2D[] sides = {
            SetBoundary(Vector2.Down),
            SetBoundary(Vector2.Up),
            SetBoundary(Vector2.Right),
            SetBoundary(Vector2.Left)
        };
        foreach (var side in sides){
            staticBody.AddChild(side);
            side._Ready();
        }                
        return staticBody;
    }
}