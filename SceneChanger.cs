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

            if (BlindRadiance.Instance.RemoveBackground && GameManager.instance.IsGameplayScene())
            {
                var planeZ = plane.transform.position.z;
                foreach (var go in scene.GetRootGameObjects())
                    foreach (var t in go.GetComponentsInChildren<Transform>())
                        if (t != null && t.gameObject != null)
                            if (t.position.z > planeZ)
                                Destroy(t.gameObject);
            }
            else
            {
                var backgroundDim = Instantiate(plane, null, true);
                backgroundDim.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"))
                {
                    color = new Color(0, 0, 0, 128)
                };
                backgroundDim.GetComponent<BlurPlane>().SetPlaneVisibility(true);
                Destroy(backgroundDim.GetComponent<BlurPlane>());
            }

            Log("Background Patched");
        }

        private void Log(string message)
        {
            Logger.Log($"[{GetType().FullName?.Replace(".", "]:[")}] - {message}");
            Debug.Log($"[{GetType().FullName?.Replace(".", "]:[")}] - {message}");
        }
        private void Log(object message)
        {
            Log($"{message}");
        }
    }
}