using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
    public class ExitGame : MonoBehaviour
    {
        // Start is called before the first frame update
        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}
