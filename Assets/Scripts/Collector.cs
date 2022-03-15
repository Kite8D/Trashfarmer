using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace PeliprojektiExamples
{
	public class Collector : MonoBehaviour
	{
		public TextMeshProUGUI scoreCounter;
		private int score;

		void Start()
     	{
			// Aloittaa niillä pisteillä mihin jääty
         	score = PlayerPrefs.GetInt("Points", score);
     	}
		
    	void Update()
     	{
			// päivittää pisteet tekstiin
        	scoreCounter.text = "Score: " + score;
     	}

		private void OnApplicationQuit()
 		{
			// Muistaa pisteet pelistä poistuttaessa
     		PlayerPrefs.GetInt("Points", score);
 		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Collectable collectable = other.GetComponent<Collectable>();
			if (collectable != null)
			{
				Debug.Log("Object collected! " + collectable.Score + " awarded!");
				// Tallettaa scoreen kerätyt pisteet
				score += collectable.Score;

				// Tallettaa ne sessioon
				PlayerPrefs.SetInt("Points", score);

				Destroy(other.gameObject);
			}
		}
	}
}
