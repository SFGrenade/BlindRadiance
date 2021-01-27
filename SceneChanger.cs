using System;
using System.Collections;
using UnityEngine;
using Logger = Modding.Logger;

namespace BlindRadiance
{
    class SceneChanger : MonoBehaviour
    {
        public void Change_BG(UnityEngine.SceneManagement.Scene scene)
        {
            Log("Patching Background");

            GameObject plane = GameObject.Find("BlurPlane");
            GameObject backgroundDim = GameObject.Instantiate(plane, null, true);
            backgroundDim.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            backgroundDim.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 128);
            backgroundDim.GetComponent<BlurPlane>().SetPlaneVisibility(true);
            GameObject.Destroy(backgroundDim.GetComponent<BlurPlane>());

            Log("Background Patched");
        }

        private void Log(String message)
        {
            Logger.Log($"[{this.GetType().FullName.Replace(".", "]:[")}] - {message}");
        }
        private void Log(System.Object message)
        {
            Logger.Log($"[{this.GetType().FullName.Replace(".", "]:[")}] - {message.ToString()}");
        }
    }
}
