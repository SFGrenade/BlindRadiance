using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Modding;

namespace BlindRadiance
{
    public class BlindRadiance : Mod<BrSaveSettings, BrGlobalSettings>
    {
        internal static BlindRadiance Instance;

        private SceneChanger sceneChanger;

        public override string GetVersion()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string ver = asm.GetName().Version.ToString();
            SHA1 sha1 = SHA1.Create();
            FileStream stream = File.OpenRead(asm.Location);
            byte[] hashBytes = sha1.ComputeHash(stream);
            string hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
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

        private void OnSceneChanged(UnityEngine.SceneManagement.Scene from, UnityEngine.SceneManagement.Scene to)
        {
            string scene = GameManager.instance.GetSceneNameString();
            if (GlobalSettings.scenes.Contains(scene))
            {
                sceneChanger.Change_BG(to);
            }
        }
    }
}
