using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
	public class Mover : MonoBehaviour, IMover 
	{

		[SerializeField]
		private float _speed;
    	[SerializeField]
    	private Transform [] waypoints;
    	private int waypointIndex = 0;
        public Vector2 Position 
		{
			get;
		}
		private Vector2 direction;

		public float Speed { 
			get { return _speed; }
			set { _speed = value; }
			}

		public void Move(Vector2 direction, float deltaTime)
		{
			if (waypointIndex <= waypoints.Length - 1)
        	{

				// Siirrä Enemy nykyisestä reittipisteestä seuraavaan
				direction = transform.position = Vector2.MoveTowards(transform.position,
				waypoints[waypointIndex].transform.position,
				Speed * Time.deltaTime);

				// Jos Enemy saavuttaa sen reittipisteen sijainnin, jota kohti hän käveli
				// waypointIndex kasvaa 1:llä
				// ja Enemy lähtee kävelemään seuraavaan reittipisteeseen
				if (transform.position == waypoints[waypointIndex].transform.position)
				{
					waypointIndex += 1;
				}
				
				// Palaa alkuun ja aloita alusta
				if (waypointIndex == waypoints.Length) {
					waypointIndex = 0;
				}
        	}
		}


		void Start()
		{
			// Asettaa Enemy sijainti ensimmäisen reittipisteen sijainniksi.
        	transform.position = waypoints[waypointIndex].transform.position;
		}

		void Update()
		{
        	Move(direction, Time.deltaTime);
		}
	}



}
