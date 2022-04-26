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

        void Start()
        {
            startPosition = transform.position;
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
