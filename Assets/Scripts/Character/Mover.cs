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
		private bool randomizeWaypoints;
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
				if (transform.position == waypoints[waypointIndex].transform.position && randomizeWaypoints == false)
				{
					waypointIndex += 1;
					Debug.Log("Waypoint number " + waypointIndex);
				} 
				else if (transform.position == waypoints[waypointIndex].transform.position && randomizeWaypoints == true) 
				{
					waypointIndex = Random.Range(0, waypoints.Length);
					Debug.Log("Waypoint number " + waypointIndex);
				}
				
				// Palaa alkuun ja aloita alusta
				if (waypointIndex == waypoints.Length && randomizeWaypoints == false) {
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
