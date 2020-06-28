using System.Reflection;
using Modding;

namespace BlindRadiance
{
    public class BlindRadiance : Mod
    {
        internal static BlindRadiance Instance;

        private SceneChanger sceneChanger;

        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize()
        {
            Log("Initializing");
            Instance = this;

            sceneChanger = new SceneChanger
            {
                modBase = this
            };
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
            if ((scene == "Dream_Final_Boss") || (scene == "Dream_Final") || (scene == "GG_Radiance"))
            {
                // Path of Pain Entrance, needs change to make "Test of Teamwork" accessible
                GameManager.instance.StartCoroutine(sceneChanger.Change_Dream_Final_Boss(to));
            }
        }
    }
}
