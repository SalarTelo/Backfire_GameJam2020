using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using System.IO;

namespace SpellcastStudios
{
    public class SceneManagerWindow : EditorWindow
    {
        private SceneAsset addScene;

        [MenuItem("Spellcast/Scene Manager")]
        private static void OpenWindow()
        {
            SceneManagerWindow window = GetWindow<SceneManagerWindow>(false, "Scene Manager");
        }

        private void OnGUI()
        {
            for(int i = 0; i < EditorBuildSettings.scenes.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                DrawScene(i);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            addScene = (SceneAsset)EditorGUILayout.ObjectField(addScene, typeof(SceneAsset), false);

            if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                string guid = AssetDatabase.FindAssets(addScene.name)[0];
                Debug.Log("Adding " + AssetDatabase.GUIDToAssetPath(guid));
                AddScene(AssetDatabase.GUIDToAssetPath(guid), EditorBuildSettings.scenes.Length);
            }
               
            EditorGUILayout.EndHorizontal();
        }

        private void DrawScene(int sceneIndex)
        {
            if(EditorBuildSettings.scenes[sceneIndex].path == null)
            {
                EditorGUILayout.LabelField("(Invalid Scene)");

                if (GUILayout.Button("X",GUILayout.ExpandWidth(false)))
                    RemoveScene(sceneIndex);

                return;
            }

            //Menu
            if(sceneIndex == 0)
                EditorGUILayout.LabelField("Main Menu");

            //Level
            else
            {
                string name = Path.GetFileNameWithoutExtension(EditorBuildSettings.scenes[sceneIndex].path);
                EditorGUILayout.LabelField("(LVL " + sceneIndex + ") " + name);
            }

            if (!Application.isPlaying && GUILayout.Button("Load", GUILayout.ExpandWidth(false)))
                LoadScene(sceneIndex);

            if (Application.isPlaying && GUILayout.Button("Load inGame", GUILayout.ExpandWidth(false)))
                LoadScenePlay(sceneIndex);

            EditorGUI.BeginDisabledGroup(sceneIndex == 0 || Application.isPlaying);

            if (GUILayout.Button("X", GUILayout.ExpandWidth(false)))
                RemoveScene(sceneIndex);

            EditorGUI.EndDisabledGroup();
        }

        private void LoadScenePlay(int index)
        {
            SceneManager.LoadScene(EditorBuildSettings.scenes[index].path);
        }

        private void LoadScene(int index)
        {
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[index].path);
        }

        private void RemoveScene(int index)
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            scenes.RemoveAt(index);
            EditorBuildSettings.scenes = scenes.ToArray();
        }

        private void AddScene(string path,int index)
        {
            var scenes = new List<EditorBuildSettingsScene>(EditorBuildSettings.scenes);
            scenes.Insert(index,new EditorBuildSettingsScene(path, true));
            EditorBuildSettings.scenes = scenes.ToArray();
        }

    }

}