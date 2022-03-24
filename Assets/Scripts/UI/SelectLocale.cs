using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Trashfarmer
{
    public class SelectLocale : MonoBehaviour
    {
        [SerializeField]
        private Locale locale;

        public void SetLocale()
		{
            LocalizationSettings.SelectedLocale = locale;
		}
    }
}
