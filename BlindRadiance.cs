using System.Reflection;
using SFCore.Generics;
using UnityEngine.SceneManagement;

namespace BlindRadiance
{
    public class BlindRadiance : GlobalSettingsMod<BrGlobalSettings>
    {
        internal static BlindRadiance Instance;

        private readonly SceneChanger _sceneChanger;

        public override string GetVersion() => SFCore.Utils.Util.GetVersion(Assembly.GetExecutingAssembly());

        public bool RemoveBackground { get => GlobalSettings.RemoveBackground; }

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
            if (GlobalSettings.RemoveBackground) _sceneChanger.Remove_BG(to);
            if (GlobalSettings.scenes.Contains(scene)) _sceneChanger.Change_BG(to);
        }
    }
}