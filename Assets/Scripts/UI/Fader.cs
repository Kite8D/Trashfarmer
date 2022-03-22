using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Trashfarmer
{
    public class Fader : MonoBehaviour
    {
        public enum FadeState
		{
            None,
            FadeIn,
            FadeOut
		}

        [SerializeField]
        private Image background;

        [SerializeField]
        private float speed = 1;

        private FadeState state = FadeState.None;
        private Color bgColor;
       

        void Start()
        {
            // Alussa v‰ri on t‰ysin l‰pin‰kyv‰
            bgColor = background.color;
            bgColor.a = 0; // T‰ysi l‰pin‰kyvyys
            background.color = bgColor; // Kopioidaan muutettu v‰riarvo taustakuvalle
        }


        void Update()
        {
            switch(state)
			{
                case FadeState.FadeIn:
                    bgColor.a = Mathf.Clamp01(bgColor.a + Time.deltaTime * speed);  // Pidet‰‰n arvo aina v‰lill‰ [0,1]
                    background.color = bgColor;

                    if (bgColor.a == 1)
					{
                        state = FadeState.None;
					}
                    break;
                case FadeState.FadeOut:
                    bgColor.a = Mathf.Clamp01(bgColor.a - Time.deltaTime * speed);  // Pidet‰‰n arvo aina v‰lill‰ [0,1]
                    background.color = bgColor;

                    if (bgColor.a == 0)
                    {
                        state = FadeState.None;
                    }
                    break;
			}
        }

        public float FadeIn()
		{
            state = FadeState.FadeIn;
            return 1 / speed; // Fade animaation aika
		}

        public float FadeOut()
		{
            state = FadeState.FadeOut;
            return 1 / speed;
		}
    }
}
