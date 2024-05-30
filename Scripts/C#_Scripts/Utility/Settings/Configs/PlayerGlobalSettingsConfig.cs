using Godot;
using System;

namespace Erikduss
{
	public partial class PlayerGlobalSettingsConfig : Node
	{
		//Movement
		public float movementSpeed = 200f;
		public float jumpVelocity = 200f;

		//Base Attack
		public float attackDamageMultiplier = 1f;

		//Base Defence
		public float defenceMultiplier = 1f;

		//Stats
		public int startingHealth = 100;
		public int startingStamina = 100;
		public int startingMana = 100;
    }
}
