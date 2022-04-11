using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trashfarmer
{
    public class OptionsLoad : MonoBehaviour
    {
        public void Change(string scene)
        {
            SceneManager.LoadSceneAsync(LevelLoader.OptionsName, LoadSceneMode.Additive);
        }
	}
}
