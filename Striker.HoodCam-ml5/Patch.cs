using MelonLoader;
using HarmonyLib;
using UnityEngine;

namespace Striker.HoodCamMod.ml5
{
    public class HoodCamMod : MelonMod
    {
        Vector3 newLocalPosition;

        public override void OnSceneWasLoaded(int buildindex, string SceneName)
        {
            if (IsOutside())
            {
                if (RCC_SceneManager.instance.activePlayerCamera != null)
                {
                    RCC_Camera cam = RCC_SceneManager.instance.activePlayerCamera;

                    if (cam.GodConstant.playerCar != null)
                    {
                        ChassisDetect(cam.GodConstant.playerCar.carLocal.carOrigin.chassisType, newLocalPosition);

                        cam.bumperCam.transform.localPosition = newLocalPosition;
                    }
                }
            }

            //R34 -> new Vector3(0f, 0.035f, 0.03f)
            //S15 -> new Vector3(0f, 0.035f, 0.025f)
            //911 -> new Vector3(0f, 0.03f, 0.03f)
            //Chaser -> new Vector3(0f, 0.03f, 0.03f)
        }

        public override void OnUpdate()
        {
            if (IsOutside())
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

                    if (cam.bumperCam != null) // unnecessary check but prevents errors when entering PA
                    {
                        if (cam.GodConstant.playerCar != null)
                        {
                            ChassisDetect(cam.GodConstant.playerCar.carLocal.carOrigin.chassisType, newLocalPosition);

                            if (cam.bumperCam.transform.localPosition != newLocalPosition)
                            {
                                cam.bumperCam.transform.localPosition = newLocalPosition;
                            }
                        }
                    }
                }
                else if (cam.cameraMode == RCC_Camera.CameraMode.FPS)
                {
                    cam.FPSMinFOV = cockpitMinFOV;
                    cam.FPSMaxFOV = cockpitMaxFOV;
                }
            }
        }

        public bool IsOutside()
        {
            if (RCC_SceneManager.instance != null)
            {
                if (RCC_SceneManager.instance.gameObject.scene.name != "_mainMenu" &&
                RCC_SceneManager.instance.gameObject.scene.name != "Car Garage")
                {
                    if (RCC_SceneManager.instance.activePlayerCamera != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void ChassisDetect(car_carOrigin.ChassisType chassis, Vector3 newLocalPosition)
        {
            switch (chassis)
            {
                case car_carOrigin.ChassisType.korschen_911_1989_1994:
                    newLocalPosition = new Vector3(0f, 0.03f, 0.03f);
                    break;
                case car_carOrigin.ChassisType.korschen_911_turbo_1989_1994:
                    newLocalPosition = new Vector3(0f, 0.03f, 0.03f);
                    break;
                case car_carOrigin.ChassisType.sannis_livisa_hatch_1989:
                    newLocalPosition = new Vector3(0f, 0.03f, 0.025f);
                    break;
                case car_carOrigin.ChassisType.sannis_sykina_2door_1998:
                    newLocalPosition = new Vector3(0f, 0.035f, 0.03f);
                    break;
                case car_carOrigin.ChassisType.sannis_livisa_1999:
                    newLocalPosition = new Vector3(0f, 0.035f, 0.025f);
                    break;
                case car_carOrigin.ChassisType.kymoto_sprecia_1996_2001:
                    newLocalPosition = new Vector3(0f, 0.03f, 0.03f);
                    break;
                default:
                    newLocalPosition = new Vector3(0f, 0.03f, 0.03f);
                    break;
            }

            this.newLocalPosition = newLocalPosition;
        }
    }
}

