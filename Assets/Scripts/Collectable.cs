using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeliprojektiExamples
{
	public class Collectable : MonoBehaviour
	{
		[SerializeField]
		private int score;

		// Property, joka toimii kuin read-only tyyppinen muuttuja
		public int Score
		{
			get { return score; }
			// set { score = value; }
		}
	}
}
