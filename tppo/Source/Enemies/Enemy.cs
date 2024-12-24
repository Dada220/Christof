using Godot;
using System;

public partial class Enemy : CharacterBody2D,IEnemy
{	
	[ExportGroup("State")]
	[Export] protected float Health{get;set;}
	[Export] protected float MovementSpeed{get;set;}

	[ExportGroup("Attack")]
	[Export] protected float AttackDamage{get;set;} 
	[Export] protected float AttackRange{get;set;}
	[Export] protected attackType AttackType {get;set;}
	
	protected bool IsDead {get {return Health <= 0;}}
	
	public Enemy(){
		Health = 100f;
		MovementSpeed = 50f;
		AttackDamage = 10f;
		AttackRange = 5f;
		AttackType = attackType.CloseRange;
	}

	public void Hurt(float damage){
		Health -= damage;
		GD.Print("Got hurt by ",damage," points");
		GD.Print("Current health ",Health);
		
		if (IsDead){			
			GD.Print("They killed Kenny. You Bastards!");
			this.QueueFree();
		}
	}	

	public void GetInput(){
		LookAt(GameState.player.Position);			
		Velocity = GameState.player.Position - this.Position;		
	}

	public override void _Process(double delta){
		GetInput();
		MoveAndSlide();
	}		
}
