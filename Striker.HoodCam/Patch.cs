using Il2Cpp;
using MelonLoader;
using HarmonyLib;
using UnityEngine;
using System.Runtime.CompilerServices;
using static MelonLoader.MelonLogger;
using UnityEngine.SceneManagement;

namespace Striker.HoodCamMod
{
    public class HoodCamMod : MelonMod
    {
        public override void OnSceneWasLoaded(int buildindex, string SceneName)
        {
            if(RCC_SceneManager.instance != null)
            {
                if (RCC_SceneManager.instance.gameObject.scene.name != "_mainMenu" &&
                RCC_SceneManager.instance.gameObject.scene.name != "Car Garage")
                {
                    if (RCC_SceneManager.instance.activePlayerCamera != null)
                    {
                        RCC_Camera cam = RCC_SceneManager.instance.activePlayerCamera;

                        Vector3 newLocalPosition = new Vector3(0f, 0.03f, 0.025f);

                        cam.bumperCam.transform.localPosition = newLocalPosition;
                    }
                }
            }
        }

        public override void OnUpdate()
        {
            if (RCC_SceneManager.instance != null)
            {
                if (RCC_SceneManager.instance.gameObject.scene.name != "_mainMenu" &&
                RCC_SceneManager.instance.gameObject.scene.name != "Car Garage")
                {
                    if (RCC_SceneManager.instance.activePlayerCamera != null)
                    {
                        RCC_Camera cam = RCC_SceneManager.instance.activePlayerCamera;

                        float cockpitMinFOV = 50;
                        float cockpitMaxFOV = 85;   

                        if (cam.cameraMode == RCC_Camera.CameraMode.BUMPER)
                        {
                            cam.FPSMinFOV = 70;
                            cam.FPSMaxFOV = 80;
                        }
                        else if (cam.cameraMode == RCC_Camera.CameraMode.FPS)
                        {
                            cam.FPSMinFOV = cockpitMinFOV;
                            cam.FPSMaxFOV = cockpitMaxFOV;
                        }
                    }
                }
            }  
        }
    }
}
