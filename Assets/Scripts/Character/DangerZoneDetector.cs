using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
    public class DangerZoneDetector : MonoBehaviour
    {
       [SerializeField]
       public bool PlayerInArea; 

       private void OnTriggerEnter2D(Collider2D collision)
       {
           if(collision.CompareTag("Player"))
           {
               PlayerInArea = true;
               
               Debug.Log("Danger Zone crossed");
           }
       }

       private void OnTriggerExit2D(Collider2D collision)
       {
           if(collision.CompareTag("Player"))
           {
               PlayerInArea = false;
              
           }
       }
    }
}
