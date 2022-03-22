using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Trashfarmer
{
    public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] 
        private Image img;

        [SerializeField]
        private Sprite defaulted, pressed;

        [SerializeField]
        private AudioClip compressClip, uncompressClip;

        [SerializeField]
        private AudioSource source;

        public void OnPointerDown(PointerEventData evendData)
		{
            img.sprite = pressed;
            source.PlayOneShot(compressClip);
		}

        public void OnPointerUp(PointerEventData eventData)
		{
            img.sprite = default;
            source.PlayOneShot(uncompressClip);
		}

        public void LoadSceneAsync(string Joonaksen_Testi)
		{
            SceneManager.LoadScene(Joonaksen_Testi);
		}
    }
}
