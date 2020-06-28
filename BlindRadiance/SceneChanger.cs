using System.Collections;
using UnityEngine;

namespace BlindRadiance
{
    class SceneChanger : MonoBehaviour
    {
        public BlindRadiance modBase { get; set; }

        public IEnumerator Change_Dream_Final_Boss(UnityEngine.SceneManagement.Scene scene)
        {
            modBase.Log("Change_Dream_Final_Boss()");
            yield return null;

            GameManager.instance.StartCoroutine(PatchBackground(scene));
        }

        private IEnumerator PatchBackground(UnityEngine.SceneManagement.Scene scene)
        {
            yield return null;
            modBase.Log("Patching Background");

            GameObject plane = GameObject.Find("BlurPlane");
            GameObject backgroundDim = GameObject.Instantiate(plane, null, true);
            backgroundDim.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            backgroundDim.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0, 128);

            modBase.Log("Background Patched");
        }
    }
}
