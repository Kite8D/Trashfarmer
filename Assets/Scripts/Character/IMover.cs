using UnityEngine;

// Aseta nimiavaruus omaa toteutustasi vastaavaksi
namespace Peliprojekti
{
	public interface IMover
	{
		// Hahmon tämänhetkinen sijainti
		Vector2 Position
		{
			get;
		}

		// Hahmon nopeus. Nopeuden voi lukea ja asettaa
		float Speed
		{
			get;
			set;
		}

		void Move(Vector2 direction, float deltaTime);
	}
}
