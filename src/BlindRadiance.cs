using System;
using System.Reflection;
using Modding;
using Satchel.BetterMenus;
using SFCore.Generics;
using UnityEngine.SceneManagement;

namespace BlindRadiance;

public class BlindRadiance : GlobalSettingsMod<BrGlobalSettings>, ICustomMenuMod
{
    internal static BlindRadiance Instance;

    private readonly SceneChanger _sceneChanger;

    bool ICustomMenuMod.ToggleButtonInsideMenu => true;
    public Menu MenuRef;

    public override string GetVersion() => SFCore.Utils.Util.GetVersion(Assembly.GetExecutingAssembly());

    public bool RemoveBackground
    {
        get => GlobalSettings.RemoveBackground;
    }

    public BlindRadiance() : base("Blind Radiance")
    {
        Instance = this;

        _sceneChanger = new SceneChanger();
    }

    public override void Initialize()
    {
        Log("Initializing");

        InitCallbacks();

        Log("Initialized");
    }

    private void InitCallbacks()
    {
        // Hooks
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene from, Scene to)
    {
        var scene = to.name;
        if (GlobalSettings.UseWhiteList && GlobalSettings.SceneWhiteList.Contains(scene))
        {
            if (GlobalSettings.RemoveBackground) _sceneChanger.Remove_BG(to);
            _sceneChanger.Change_BG(to);
        }
        else if (!GlobalSettings.UseWhiteList)
        {
            if (GlobalSettings.RemoveBackground) _sceneChanger.Remove_BG(to);
            _sceneChanger.Change_BG(to);
        }
    }

    public Menu PrepareMenu()
    {
        return new Menu("Blind Radiance", new Element[]
        {
            new HorizontalOption("Use Scene Whitelist",
                "See the GlobalSettings.json in the save folder for the whitelist.",
                new[] { "Off", "On" },
                (option) => { GlobalSettings.UseWhiteList = Convert.ToBoolean(option);},
                () => Convert.ToInt32(GlobalSettings.UseWhiteList)),
            new HorizontalOption("Remove Background",
                "Would remove elements that are behind the black background.",
                new[] { "Off", "On" },
                (option) => GlobalSettings.RemoveBackground = Convert.ToBoolean(option),
                () => Convert.ToInt32(GlobalSettings.RemoveBackground)),
            new CustomSlider("Black Opacity",
                (option) => GlobalSettings.Opacity = option,
                () => GlobalSettings.Opacity, 0.0f, 1.0f, false),
        });
    }

    public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggleDelegates)
    {
        MenuRef ??= PrepareMenu();

        return MenuRef.GetMenuScreen(modListMenu);
    }
}