using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeliprojektiExamples
{
	public class Collectable : MonoBehaviour
	{
		[SerializeField]
		private int score;

		public Types Type = new Types();

		// Property, joka toimii kuin read-only tyyppinen muuttuja
		public int Score
		{
			get { return score; }
			// set { score = value; }
		}
	}

	// List of what kind is the object
	public enum Types
    {
        Capsule, 
        Square, 
        Circle
    };
}
