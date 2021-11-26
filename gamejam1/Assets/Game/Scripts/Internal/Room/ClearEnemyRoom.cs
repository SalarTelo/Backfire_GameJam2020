using Boo.Lang;
using UnityEngine;

namespace SpellcastStudios
{
    /// <summary>
    /// Room that closes all the doors inside when the player enters,
    /// then opens once all enemies inside the room have been killed
    /// </summary>
    public class ClearEnemyRoom : Room
    {
        [SerializeField] private bool handleDoors = true;
        [SerializeField] private bool onlyCloseOnce = true;

        [Header("Message")]

        [SerializeField] private string startMessage = "Kill the Enemies";
        [SerializeField] private string stopMessage = "Room Cleared";

        [SerializeField] private AudioClip startSound;
        [SerializeField] private AudioClip stopSound;

        private List<Enemy> enemiesInRoom;
        private bool hasBeenCleared = false;
        private bool playerInside;

        private void Start()
        {
            
        }

        protected override void Update()
        {
            base.Update();

            if(playerInside && !hasBeenCleared)
            {
                //Success!
                if(CheckIfCleared())
                {
                    GameManager.Instance.SendGlobalMessage(stopMessage, 2, stopSound);

                    hasBeenCleared = true;

                    foreach (Door door in GetAllTypeInRoom<Door>())
                        door.OpenDoor();
                }
            }
        }

        private bool CheckIfCleared()
        {
            if (enemiesInRoom == null)
                return true;

            for (int i = 0; i < enemiesInRoom.Count; i++)
            {
                //Dead TODO: Don't assume enemy has Health Component
                if (enemiesInRoom[i] == null || enemiesInRoom[i].GetComponent<HealthComponent>().Dead)
                    continue;

                //Found alive, so we fail
                return false;
            }

            return true;
        }

        protected override void OnPlayerEnter()
        {
            if (hasBeenCleared && onlyCloseOnce)
                return;

            GameManager.Instance.SendGlobalMessage(startMessage, 2, startSound);

            enemiesInRoom = GetAllTypeInRoom<Enemy>();

            if(enemiesInRoom.Count == 0)
            {
                Debug.Log("No enemies in room, so no closing doors");
                return;
            }

            Debug.Log("Waiting for " + enemiesInRoom.Count + " to clear room!");

            foreach (Door door in GetAllTypeInRoom<Door>())
                door.CloseDoor();

            playerInside = true;
        }

        protected override void OnPlayerExit()
        {
            if (hasBeenCleared && onlyCloseOnce)
                return;

            foreach (Door door in GetAllTypeInRoom<Door>())
                door.OpenDoor();

            playerInside = true;
        }
    }

}