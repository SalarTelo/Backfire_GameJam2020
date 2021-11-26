using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using UnityEngine.Audio;
using SpellcastStudios.UI;

namespace SpellcastStudios
{
    public class GameManager : WorldSingleton<GameManager>
    {
        [SerializeField] private GameObject playerPrefab;

        [SerializeField] private GameObject hudPrefab;

        [SerializeField] private AudioMixerGroup mainAudioMixerGroup;

        [SerializeField] private List<Transform> enableOnStart;

        /// <summary>
        /// Reference to player Object
        /// </summary>
        public GameObject player { get; private set; }
        public int CurrentLevel { get; private set; }

        public Action<float> OnLoadFadeStart;
        public Action<string,float,AudioClip> OnGlobalMessage;

        public AudioMixerGroup MainAudioMixerGroup => mainAudioMixerGroup;

        private void Awake()
        {
            //PlayerPrefab is null when game is reloaded? WHYYYY
            if (playerPrefab == null)
                return;

            foreach (Transform t in enableOnStart)
                t.gameObject.SetActive(true);

            instance = this;

            //This is to skip the triggers when raycasting. This is a really bad way but let it be for now.
            Physics2D.queriesHitTriggers = false;

            SpawnPlayer();
            Instantiate(hudPrefab);

            if (gameObject.scene.buildIndex == -1)
                Debug.LogWarning("Level is not in build, please use the scene manager to add it");

            //Just set the current level to the buildindex-1. If build index is invalid, then CurrentLevel will be negative 
            CurrentLevel = gameObject.scene.buildIndex - 1;
        }
        
        private void SpawnPlayer()
        {
            var startPoint = GameObject.FindObjectOfType<PlayerStartPoint>();
            Assert.IsNotNull(startPoint, "A Level must have a PlayerStartPoint component present!");

            Vector3 spawnPoint = startPoint.transform.position;

            //Fix Z position to make life easier!
            spawnPoint.z = 0;

            player = Instantiate(playerPrefab);
            player.transform.position = spawnPoint;

            if(player.GetComponent<HealthComponent>() != null)
                player.GetComponent<HealthComponent>().OnDeath += RestartGame;
        }

        public void LoadLevel(int index,float fadeTime)
        {
            OnLoadFadeStart?.Invoke(fadeTime);

            CoroutineManager.RunDelayingCoroutine(fadeTime, () =>
            {
                SceneManager.LoadScene(index + 1);
            });
        }

        public void SendGlobalMessage(string text, float time = 2,AudioClip sound=null)
        {
            OnGlobalMessage?.Invoke(text, time, sound);
        }

        void RestartGame()
        {
            SendGlobalMessage("Game Over",2);

            IEnumerator DeathTimer()
            {
                yield return new WaitForSeconds(2);
                SceneManager.LoadScene(gameObject.scene.buildIndex);
            }

            StartCoroutine(DeathTimer());
        }

        //TODO: We need to make every entity actually use the IEntity tag. Right now no gameobject uses it.
        List<IEntity> entities;

        List<T> GetEntitiesOfType<T>() where T : IEntity
        {
            return entities.OfType<T>().ToList();
        }
        void RemoveEntitiesOfType<T>() where T : IEntity
        {
            //There is probably an easier method.
            var temp = entities.OfType<T>().ToList();
            if(temp.Count > 0)
            {
                foreach(var entity in temp)
                {
                    entities.Remove(entity);
                }
            }
        }

    }

}