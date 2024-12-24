using Godot;
using System;

public partial class BossEnemy : Enemy
{	
	public BossEnemy(){
		Health = 150f;
		MovementSpeed = 25f;
		AttackDamage = 20f;
		AttackRange = 5f;
		AttackType = attackType.CloseRange;
	}	
}
