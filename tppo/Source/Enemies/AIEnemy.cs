using Godot;
using System;

public partial class AIEnemy : Enemy
{	
	public AIEnemy(){
		Health = 100f;
		MovementSpeed = 40f;
		AttackDamage = 20f;
		AttackRange = 15f;
		AttackType = attackType.LongRange;
	}	
}
