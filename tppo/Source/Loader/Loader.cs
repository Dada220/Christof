using Godot;
using System;

public partial class Loader : Node
{    
    private static PackedScene Enemy;
    private static PackedScene AIEnemy;
    private static PackedScene BossEnemy;
    private static PackedScene Door;
    private static PackedScene Player;        
    private static TileSet TileSet;

    private static String EnemyPath     = "res://Scenes/Enemies/Enemy.tscn";
    private static String AIEnemyPath   = "res://Scenes/Enemies/AIEnemy.tscn";
    private static String BossEnemyPath = "res://Scenes/Enemies/BossEnemy.tscn";
    private static String DoorPath      = "res://Scenes/Levels/Door.tscn";
    private static String PlayerPath    = "res://Scenes/Player.tscn";
    private static String TileSetPath   = "res://assets/MapTileSet/tileset.tres";    
    
    public override void _Ready()
    {  
        Enemy       = ResourceLoader.Load<PackedScene>(EnemyPath);
        AIEnemy     = ResourceLoader.Load<PackedScene>(AIEnemyPath);
        BossEnemy   = ResourceLoader.Load<PackedScene>(BossEnemyPath);
        Player      = ResourceLoader.Load<PackedScene>(PlayerPath);
        Door        = ResourceLoader.Load<PackedScene>(DoorPath);        
        TileSet     = ResourceLoader.Load<TileSet>(TileSetPath);        
    }

    public static Node2D LoadRandomEnemy(){        
        Func<Node2D>[] funcs = {LoadEnemy,LoadBossEnemy,LoadAIEnemy};
        Random random = new();
        var choice = random.Next(0,funcs.Length);        
        return funcs[choice]();
    }

    public static TileSet LoadTileSet() => TileSet;
    public static Node2D LoadDoor() => (Node2D)Door.Instantiate();
    public static Node2D LoadEnemy() => (Node2D)Enemy.Instantiate();
    public static Node2D LoadBossEnemy() => (Node2D)BossEnemy.Instantiate();            
    public static Node2D LoadAIEnemy()   => (Node2D)AIEnemy.Instantiate();        
    public static Node2D LoadPlayer() => (Node2D)Player.Instantiate();        
}