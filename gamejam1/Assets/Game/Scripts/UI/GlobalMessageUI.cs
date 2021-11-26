using TMPro;
using UnityEngine;

namespace SpellcastStudios.UI
{
    public class GlobalMessageUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private float disableTime;

        private void Start()
        {
            text.enabled = false;

            GameManager.Instance.OnGlobalMessage += OnGlobalMessage;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance != null)
                GameManager.Instance.OnGlobalMessage -= OnGlobalMessage;
        }

        private void Update()
        {
            if (text.enabled && Time.time > disableTime)
                text.enabled = false;
        }

        private void OnGlobalMessage(string text, float time = 2,AudioClip clip = null)
        {
            this.text.text = text;
            this.text.enabled = true;
            disableTime = Time.time + time;

            SoundManager.PlayAudioClip(clip);
        }
    }
}