using System.Collections.Generic;

namespace BlindRadiance;

public class BrGlobalSettings
{
    public bool UseWhiteList = true;
    public bool RemoveBackground = false;
    public List<string> SceneWhiteList = new List<string>
    {
        "Dream_Final_Boss",
        "Dream_Final",
        "GG_Radiance",
        "Deepnest_East_Hornet",
        "GG_Hornet_2"
    };
    public float Opacity = 1.0f;
}