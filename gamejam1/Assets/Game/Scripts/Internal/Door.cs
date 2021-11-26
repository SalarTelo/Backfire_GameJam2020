using UnityEngine;

namespace SpellcastStudios
{
    /// <summary>
    /// Base door class that does not open or close on its own, needs an outside
    /// source to trigger the open / close values
    /// </summary>
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool openByDefault = true;
        [SerializeField] private SpriteRenderer doorRenderer;
        [SerializeField] private Collider2D doorCollider;

        [SerializeField] private CameraShakeSetting shakeOnOpen;
        [SerializeField] private CameraShakeSetting shakeOnClose;

        [SerializeField] private GameObject particlesOnOpen;
        [SerializeField] private GameObject particlesOnClose;

        [SerializeField] private AudioClip soundOnOpen;
        [SerializeField] private AudioClip soundOnClose;

        public bool IsOpen { get; private set; }

        private void Start()
        {
            if (openByDefault)
                OpenDoor(true);
            else
                CloseDoor(true);
        }

        public void OpenDoor(bool mute=false)
        {
            if (IsOpen)
                return;

            IsOpen = true;

            doorRenderer.enabled = false;
            doorCollider.enabled = false;

            if (shakeOnOpen != null && !mute)
                shakeOnOpen.ShakeAtPoint(transform.position);

            if (particlesOnOpen != null && !mute)
                Instantiate(particlesOnOpen, transform.position, Quaternion.identity);

            if(!mute)
                SoundManager.PlayAudioClip(soundOnOpen);
        }

        public void CloseDoor(bool mute = false)
        {
            if (!IsOpen)
                return;

            IsOpen = false;

            doorRenderer.enabled = true;
            doorCollider.enabled = true;

            if (shakeOnOpen != null && !mute)
                shakeOnOpen.ShakeAtPoint(transform.position);

            if (particlesOnOpen != null && !mute)
                Instantiate(particlesOnOpen, transform.position, Quaternion.identity);

            if (!mute)
                SoundManager.PlayAudioClip(soundOnOpen);
        }
    }
}