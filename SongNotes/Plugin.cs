using HarmonyLib;
using IPA;
using IPALogger = IPA.Logging.Logger;

namespace SongNotes
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static IPALogger Log { get; private set; }
        private Harmony _harmony;

        [Init]
        public Plugin(IPALogger logger)
        {
            Log = logger;
            _harmony = new Harmony("com.nuggo.songnotes");
        }

        [OnStart]
        public void OnApplicationStart()
        {
            _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());
        }

        [OnExit]
        public void OnApplicationQuit()
        {

        }

    }
}
