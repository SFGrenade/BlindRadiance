using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = Modding.Logger;

namespace BlindRadiance
{
    internal class SceneChanger : MonoBehaviour
    {
        public void Change_BG(Scene scene)
        {
            Log("Patching Background");

            var plane = GameObject.Find("BlurPlane");
            var backgroundDim = Instantiate(plane, null, true);
            backgroundDim.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            backgroundDim.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 128);
            backgroundDim.GetComponent<BlurPlane>().SetPlaneVisibility(true);
            Destroy(backgroundDim.GetComponent<BlurPlane>());

            Log("Background Patched");
        }

        private void Log(string message)
        {
            Logger.Log($"[{GetType().FullName.Replace(".", "]:[")}] - {message}");
        }
    }
}