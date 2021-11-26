using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.Assertions;

namespace SpellcastStudios
{
    public class PlayerStartPoint : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;

            Gizmos.DrawWireSphere(transform.position, .2f);

            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.green;

            Handles.Label(transform.position + new Vector3(.31f,.2f, 0), new GUIContent("Player Start Point"), style);

            Gizmos.color = oldColor;
        }
#endif
    }

}