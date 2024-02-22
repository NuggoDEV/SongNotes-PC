using IPA.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace SongNotes
{
    internal class Configuration
    {
        private static Configuration _instance;
        JObject config;
        internal static Configuration GetInstance()
        {
            if (_instance == null)
                _instance = new Configuration();
            return _instance;
        }

        private void LoadConfig()
        {
            if (!File.Exists(UnityGame.UserDataPath + "/SongNotes.json"))
                File.WriteAllText(UnityGame.UserDataPath + "/SongNotes.json", "{\n\n}");

            config = JObject.Parse(File.ReadAllText(UnityGame.UserDataPath + "/SongNotes.json"));
        }

        private void SaveConfig() => File.WriteAllText(UnityGame.UserDataPath + "/SongNotes.json", JsonConvert.SerializeObject(config, Formatting.Indented));

        internal string GetNoteForSong(string hash)
        {
            LoadConfig();
            if (config[hash] == null)
                return string.Empty;
            return config[hash].Value<string>();
        }

        internal void SetNoteForSong(string hash, string note)
        {
            LoadConfig();
            config[hash] = new JValue(note);
            SaveConfig();
        }
    }
}
