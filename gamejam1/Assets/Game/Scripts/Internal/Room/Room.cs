using Boo.Lang;
using UnityEngine;

namespace SpellcastStudios
{
    public class Room : MonoBehaviour
    {
        /// <summary>
        /// Time player must stay in room before the "enter" event is triggered
        /// </summary>
        [SerializeField] private float enterDelay = 2;

        private bool playerCurrentlyInside;
        private bool delayingEnterEvent;
        private float enterTime;

        protected virtual void OnPlayerEnter()
        {

        }

        protected virtual void OnPlayerExit()
        {

        }

        /// <summary>
        /// Slow and crappy utility that returns all enabled components of type T that are inside the room.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected List<T> GetAllTypeInRoom<T>() where T : Component
        {
            List<T> components = new List<T>();

            foreach(T comp in GameObject.FindObjectsOfType<T>())
            {
                if (PositionInRoom(comp.transform.position))
                    components.Add(comp);
            }

            return components;
        }

        /// <summary>
        /// Returns whether a world position is inside room
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        protected bool PositionInRoom(Vector2 position)
        {
            return GetAreaRect().Contains(position);
        }

        /// <summary>
        /// Returns rect of area. Uses fixed form that rounds up the position values
        /// </summary>
        /// <returns></returns>
        private Rect GetAreaRect()
        {
            float x = Mathf.Floor(transform.position.x - transform.lossyScale.x / 2f);
            float y = Mathf.Floor(transform.position.y - transform.lossyScale.y / 2f);

            float w = Mathf.Ceil(transform.lossyScale.x);
            float h = Mathf.Ceil(transform.lossyScale.y);

            Rect area = new Rect(x,y,w,h);

            return area;
        }

        /// <summary>
        /// Polls for player
        /// </summary>
        protected virtual void Update()
        {
            if (GameManager.Instance.player == null)
                return;

            bool playerInside = PositionInRoom(GameManager.Instance.player.transform.position);

            if(delayingEnterEvent)
            {
                //Player has left while timer is running, stop timer
                if(!playerInside)
                {
                    playerCurrentlyInside = false;
                    delayingEnterEvent = false;
                    return;
                } 
                
                //Time is up!!
                else if(enterTime + enterDelay < Time.time)
                {
                    OnPlayerEnter();
                    delayingEnterEvent = false;
                    return;
                }

                return;
            }

            //Nothing has changed
            if (playerCurrentlyInside == playerInside)
                return;

            //Player has just entered, start delay
            if(!playerCurrentlyInside && playerInside)
            {
                delayingEnterEvent = true;
                enterTime = Time.time;
            }

            //Player has just exited
            if (playerCurrentlyInside && !playerInside)
            {
                OnPlayerExit();
            }

            //Set new value
            playerCurrentlyInside = playerInside;
        }

        private void OnDrawGizmos()
        {
            DrawGizmos(false);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmos(true);
        }

        protected virtual void DrawGizmos(bool selected)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.yellow;

            Rect area = GetAreaRect();

            Gizmos.DrawWireCube(area.center, area.size);

            if (selected)
            {
                Gizmos.color = new Color(1, 1, 0, 0.2f);
                Gizmos.DrawCube(area.center, area.size);
            }

            Gizmos.color = oldColor;
        }

    }

}