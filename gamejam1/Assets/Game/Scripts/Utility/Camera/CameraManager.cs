using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpellcastStudios
{
    public class CameraManager : MonoBehaviour
    {
        //Static variables
        private static CameraManager instance;
        public static event System.Action OnCameraChange;

        public static CameraManager Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "Camera Manager";
                    instance = obj.AddComponent<CameraManager>();
                }
                return instance;
            }
        }

        //Non static variables
        private Camera currentCamera;
        private List<Camera> currentCameraChildren;
        private AudioListener currentListener;

        public static Camera CurrentCamera
        {
            get
            {
                if(Instance.currentCamera == null)
                    Instance.currentCamera = Camera.main;

                return instance.currentCamera;
            }
        }

        public static List<Camera> CurrentCameraChildren
        {
            get
            {
                if (instance == null)
                    return null;
                return instance.currentCameraChildren;
            }
        }
        public static AudioListener CurrentListener
        {
            get
            {
                if (instance == null)
                    return null;
                return instance.currentListener;
            }
        }

        //Enables or disables camera without deactivating the gameobject
        private static void CameraSetActive(Camera camera,bool active)
        {
            camera.enabled = active;

            if(active)
                camera.gameObject.SetActive(true);

            AudioListener listener = camera.GetComponent<AudioListener>();

            if (listener != null)
                listener.enabled = active;

        }

        public static void SetCurrentCamera(Camera camera)
        {
            //Check if we know about the current camera
            if (Instance.currentCamera != null)
                CameraSetActive(Instance.currentCamera, false);

            Instance.currentCamera = camera;

            instance.currentCameraChildren = new List<Camera>();

            foreach (Camera child in camera.gameObject.GetComponentsInChildren<Camera>())
            {
                if (child == camera)
                    continue;
                instance.currentCameraChildren.Add(child);
            }

            CameraSetActive(Instance.currentCamera, true);

            Instance.currentListener = Instance.currentCamera.GetComponent<AudioListener>();

            if (OnCameraChange != null)
                OnCameraChange();

        }

    }
}
