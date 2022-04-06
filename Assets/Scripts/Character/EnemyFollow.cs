using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
    public class EnemyFollow : MonoBehaviour
    {
        public float speed;

        private Transform player;
        
        //Scripti tunnistaa pelaajaan tagista: Player
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Player"))
            {
            Debug.Log("Enemy found you");
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
        }

        //komento jolla otus jahtaa pelaajaa 
        void OnTriggerStay2D(Collider2D other)
        {
             if(other.CompareTag("Player"))
            {
            Debug.Log("Enemy started to move");
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }

        //Tästä uupuu vielä komento, jolla otus palaa aloitus kohtaan
        void OnTriggerExit2D(Collider2D other)
        {
             if(other.CompareTag("Player"))
            {
            Debug.Log("Enemy backing up");
            }
        }
    }
}
