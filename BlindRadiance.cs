using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Modding;
using UnityEngine.SceneManagement;

namespace BlindRadiance
{
    public class BlindRadiance : Mod
    {
        public override ModSettings GlobalSettings
        {
            get => _globalSettings;
            set => _globalSettings = (BrGlobalSettings) value;
        }
        private BrGlobalSettings _globalSettings = new BrGlobalSettings();
        private Type globalSettingsType = typeof(BrGlobalSettings);

        internal static BlindRadiance Instance;

        private SceneChanger sceneChanger;

        public override string GetVersion()
        {
            var asm = Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version.ToString();
            var sha1 = SHA1.Create();
            var stream = File.OpenRead(asm.Location);
            var hashBytes = sha1.ComputeHash(stream);
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            stream.Close();
            sha1.Clear();
            return $"{ver}-{hash.Substring(0, 6)}";
        }

        public override void Initialize()
        {
            Log("Initializing");
            Instance = this;

            sceneChanger = new SceneChanger();
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
            if (_globalSettings.scenes.Contains(scene)) sceneChanger.Change_BG(to);
        }
    }
}