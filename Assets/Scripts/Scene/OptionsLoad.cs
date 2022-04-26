using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Trashfarmer
{
    public class OptionsLoad : MonoBehaviour
    {

		[SerializeField]
		private GameObject optionsButton;

        public void Change(string scene)
        {
            LevelLoader.Current.LoadOptions();
        }

        public static LevelLoader Current;

        private void Awake()
		{
            if (Current == null)
			{
				//Current = this;
			}
			else
			{
				// LevelLoader on jo olemassa! Tuhotaan uusi instanssi.
				Destroy(gameObject);
				return;
			}

			DontDestroyOnLoad(gameObject);
		}
	}
}
