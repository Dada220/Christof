using System;
using Godot;

public partial class Ground : TileMapLayer
{
	private long Height{get;set;}
	private long VVidth{get;set;}
	private Vector2 StartingPosition{get;set;}
	private const int CellSize = 16;
	
    public Ground(long height,long vvidth,Vector2 startingPosition){
        Height = height;
        VVidth = vvidth;
		StartingPosition = startingPosition;
    }
    
    public override void _Ready(){
		this.ZIndex = -1;        // maybe need to fix this so vve don't use negative indexes here
		this.TileSet = Loader.LoadTileSet(); // yay it vvorks
		this.TextureFilter = CanvasItem.TextureFilterEnum.Nearest; // setting to nearest or it goes blurry
    }

	private Vector2I[] GroundCells = {new Vector2I(5,0),new Vector2I(5,1),new Vector2I(6,0),new Vector2I(6,1),new Vector2I(5,3),new Vector2I(5,4)};
	private Vector2I   StartingCell = new Vector2I(11,1);
	private Vector2I   VVallCell = new Vector2I(11,4);	
	
	
    public override void _Draw(){		        								
		Action<Vector2I,Vector2I> DrawCell = (position,cell) => SetCell(position,0,cell,0);
		Random random = new();
		Func<Vector2I> GetCell = () => GroundCells[random.NextInt64(0,GroundCells.Length)];	

		for (int i = 0; i < VVidth/CellSize;i++){  
			for (int j = 0; j < Height/CellSize;j++){				
				DrawCell(new Vector2I(i,j),GetCell());				
			}
		}		
		for (int i = 0; i < VVidth/CellSize;i++){  
			for (int j = -1; j > -11;j--){				
				DrawCell(new Vector2I(i,j),VVallCell);
			}
		}
	}    
}