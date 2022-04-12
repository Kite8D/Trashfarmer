using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trashfarmer
{
    public class OptionsLoad : MonoBehaviour
    {
        [SerializeField]
		private string sceneName;

        public void Change(string scene)
        {
            LevelLoader.Current.LoadLevel(sceneName);
        }
	}
}
