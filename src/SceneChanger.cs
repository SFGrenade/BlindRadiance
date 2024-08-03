using SFCore.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = Modding.Logger;

namespace BlindRadiance;

internal class SceneChanger : MonoBehaviour
{
    public void Change_BG(Scene scene)
    {
        Log("Patching Background");

        GameObject plane = scene.Find("BlurPlane");

        GameObject backgroundDim = Instantiate(plane, null, true);
        backgroundDim.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"))
        {
            color = new Color(0, 0, 0, 128)
        };
        backgroundDim.GetComponent<BlurPlane>().SetPlaneVisibility(true);
        Destroy(backgroundDim.GetComponent<BlurPlane>());

        Log("Background Patched");
    }

    public void Remove_BG(Scene scene)
    {
        Log("Removing Background");

        BlurPlane plane = null;
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            BlurPlane bp = go.GetComponentInChildren<BlurPlane>();
            if (bp != null)
            {
                plane = bp;
            }
        }

        if (plane == null)
        {
            Log("No BlurPlane found");
            return;
        }

        Vector3 position = plane.transform.position;
        // Log($"BlurPlane '{plane.gameObject.name}' found at ({position.x}, {position.y}, {position.z})");

        float planeZ = plane.transform.position.z;
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            Try_Remove_BG(go, planeZ);
        }

        Log("Background Removed");
    }

    private void Try_Remove_BG(GameObject go, float planeZ)
    {
        if (Check_Can_Remove_BG(go, planeZ))
        {
            Vector3 position = go.transform.position;
            // Log($"{go.name} at ({position.x}, {position.y}, {position.z}) gets yeeted");
            Destroy(go);
            return;
        }

        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform child = go.transform.GetChild(i);
            Try_Remove_BG(child.gameObject, planeZ);
        }
    }

    private bool Check_Can_Remove_BG(GameObject go, float planeZ)
    {
        bool ret = true;
        ret = ret && (go.transform.position.z > planeZ); // only stuff behind the blurplane
        ret = ret && (go.GetComponents<Collider2D>().Length <= 0); // safety, who knows what colliders
        ret = ret && (go.GetComponents<PlayMakerFSM>().Length <= 0); // safety, who knows what these FSMs might do
        for (int i = 0; i < go.transform.childCount; i++)
        {
            Transform child = go.transform.GetChild(i);
            ret = ret && Check_Can_Remove_BG(child.gameObject, planeZ);
        }

        Vector3 position = go.transform.position;
        // Log($"{go.name} at ({position.x}, {position.y}, {position.z}) returns {ret}");
        return ret;
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