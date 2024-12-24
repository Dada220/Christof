using Godot;
using System;

public partial class Player : CharacterBody2D
{		 
	[ExportGroup("State")]
	[Export] private float MovementSpeed {get;set;} = 150f;	
	[Export] private float MaximumHealthLimit {get;set;} = 100f;
	[Export] private float Health {get;set;} = 100f;
		
	[ExportGroup("Attack")]	
	[Export] private float LightAttackDamage {get;set;} = 1000f;
	[Export] private float HeavyAttackDamage {get;set;} = 20f;
	[Export] private float CurrentDamage {get;set;} = 0f;
		
	private AnimationPlayer Anim;

	public Player(){
		MovementSpeed = 200f;
	}
		
	public override void _Ready(){
		Anim = GetNode<AnimationPlayer>("AnimationLightAttack");
		SceneManager.Player = this;
	}

	public void GetInput(){	
		LookAt(GetGlobalMousePosition());
		Vector2 Direction = Input.GetVector("Left","Right","Forward","Backward");
		Velocity = Direction * MovementSpeed;
	}

	public void Hurt(float damage){
		Health -= damage;
		if (Health <= 0){
			GD.Print("player is dead");
		}
	}

	public void Attack(){
		if (Input.IsActionPressed("LightAttack")){
			Anim.Play("Light_attack");
			CurrentDamage = LightAttackDamage;
		}		
		if (Input.IsActionPressed("HeavyAttack")){
			Anim.Play("Light_attack");
			CurrentDamage = HeavyAttackDamage;
		}
	}
	
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){		
		GetInput();
		Attack();				
		MoveAndSlide();		
	}	
		
	public void OnAnimationAttackFinished(StringName name){
		// maybe reduntant due to collision box appearing only during gameplay
		//	CurrentDamage = 0;       
	}
		
	private void OnBodyEntered(Node2D body){
		if (body is Enemy enemy){ // to check that vve're hitting the enemy and nothing else
			enemy.Hurt(CurrentDamage);				
		}		
	}
}
