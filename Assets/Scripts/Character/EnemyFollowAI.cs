using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
    public class EnemyFollowAI : MonoBehaviour
    {
        public float speed;

        [SerializeField]
        private Transform target;

        public DangerZoneDetector dzd;
        private Vector2 startPosition;
        public bool enemyRoam = false;
        private SpriteRenderer mySpriteRenderer;
		private float currentXPosition;

        void Start()
        {
            startPosition = transform.position;

            mySpriteRenderer = GetComponent<SpriteRenderer>();
			currentXPosition = target.position.x;
        }

        //Scripti tunnistaa pelaajaan tagista: Player
        void OnTriggerEnter2D(Collider2D other)
        {
            if (dzd.PlayerInArea == true)
            {
                Debug.Log("Enemy found you");
                target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }

        //komento jolla otus jahtaa pelaajaa 
        void OnTriggerStay2D(Collider2D other)
        {
            if (dzd.PlayerInArea == true)
            {
               // Debug.Log("Enemy started to move");
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                enemyRoam = false;

                if (target.position.x > currentXPosition && mySpriteRenderer != null)
                {
                    // Päivittää X positionia, kunnes vaihtaa suuntaa
                    currentXPosition = transform.position.x;
                    // Flippaa spriten toistenpäin
                    mySpriteRenderer.flipX = true;
                } 
                else if (target.position.x < currentXPosition && mySpriteRenderer != null)
                {
                    currentXPosition = transform.position.x;
                    mySpriteRenderer.flipX = false;
                }
            } 
            // Peruuttaa aloituspaikkaan, kun pelaaja ei ole alueella
            else if (dzd.PlayerInArea == false)
            {
                // transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                enemyRoam = true;
               // Debug.Log("Enemy backing up");
            }
        }
    }
}
