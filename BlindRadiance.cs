using System.Reflection;
using SFCore.Generics;
using UnityEngine.SceneManagement;

namespace BlindRadiance
{
    public class BlindRadiance : GlobalSettingsMod<BrGlobalSettings>
    {
        internal static BlindRadiance Instance;

        private SceneChanger sceneChanger;

        public override string GetVersion() => SFCore.Utils.Util.GetVersion(Assembly.GetExecutingAssembly());

        public bool RemoveBackground { get => _globalSettings.RemoveBackground; }

        public BlindRadiance() : base("Blind Radiance")
        {
            Instance = this;

            sceneChanger = new SceneChanger();
        }

        public override void Initialize()
        {
            Log("Initializing");

            initCallbacks();

            Log("Initialized");
        }

        private void initCallbacks()
        {
            // Hooks
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private void OnSceneChanged(Scene from, Scene to)
        {
            var scene = GameManager.instance.GetSceneNameString();
            if (_globalSettings.RemoveBackground) sceneChanger.Change_BG(to);
            if (_globalSettings.scenes.Contains(scene)) sceneChanger.Change_BG(to);
        }
    }
}