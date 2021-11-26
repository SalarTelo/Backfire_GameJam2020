using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
	public class LevelSwitchingEvents : MonoBehaviour
	{
		public float delay;
		public float fadeTime;
		
		[Header("PlayGame()")]
		public int level;

		/// <summary>
		/// Run chosen level
		/// </summary>
		public void RunLevel()
		{
			CoroutineManager.RunDelayingCoroutine(delay, () => { GameManager.Instance.LoadLevel(level, fadeTime); });
        }

		/// <summary>
		/// Run next level
		/// </summary>
		public void UpLevel()
		{
			int level = GameManager.Instance.CurrentLevel + 1;

			if (level < 0)
				level = 0;

			CoroutineManager.RunDelayingCoroutine(delay, () => { GameManager.Instance.LoadLevel(level, fadeTime); });
		}

	}

}