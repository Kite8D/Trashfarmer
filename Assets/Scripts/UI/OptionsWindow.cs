using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Trashfarmer
{
    public class OptionsWindow : MonoBehaviour
    {
        public void Close()
		{
            LevelLoader.Current.CloseOptions();
		}
    }
}
