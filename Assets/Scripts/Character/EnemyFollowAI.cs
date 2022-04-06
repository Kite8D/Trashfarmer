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
        

        //Scripti tunnistaa pelaajaan tagista: Player
        void OnTriggerEnter2D(Collider2D other)
        {
            if(dzd.PlayerInArea == true)
                {
                    Debug.Log("Enemy found you");
                    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                }
        }

        //komento jolla otus jahtaa pelaajaa 
        void OnTriggerStay2D(Collider2D other)
        {
            if(dzd.PlayerInArea == true)
                {
                    Debug.Log("Enemy started to move");
                    transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                }
        }

        //Tästä uupuu vielä komento, jolla otus palaa aloitus kohtaan
        void OnTriggerExit2D(Collider2D other)
        {
            if(dzd.PlayerInArea == false)
                {
                    Debug.Log("Enemy backing up");
                }
        }
    }
}
