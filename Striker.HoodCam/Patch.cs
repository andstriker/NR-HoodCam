using Il2Cpp;
using MelonLoader;
using HarmonyLib;
using UnityEngine;
using System.Runtime.CompilerServices;
using static MelonLoader.MelonLogger;
using UnityEngine.SceneManagement;
using Il2CppCinemachine;

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
                            cam.FPSMinFOV = 63;
                            cam.FPSMaxFOV = 75; // check bumperCam code for affectors

                            cam.speedBasedMovement = cam.mainSpeedCurve.Evaluate(Mathf.Clamp01(cam.speed_smoothed / 500f));

                            cam.wantedFOV = Mathf.Lerp(cam.FPSMinFOV, cam.FPSMaxFOV * Mathf.Lerp(0.8f, 1f, (float)cam.GodConstant.gameSettings.game_fovLevel / 5f), 
                                            Mathf.Clamp01(cam.speedBasedMovement + Mathf.Lerp(0f, 0.1f, cam.rpmBasedMovement)));
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
