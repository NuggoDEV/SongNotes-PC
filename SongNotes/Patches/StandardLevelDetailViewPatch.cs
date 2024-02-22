using HarmonyLib;
using System.Reflection;
using BeatSaberMarkupLanguage;
using UnityEngine;

namespace SongNotes.Patches
{
    [HarmonyPatch(typeof(StandardLevelDetailView), nameof(StandardLevelDetailView.RefreshContent))]
    internal class StandardLevelDetailViewPatch
    {
        internal static IDifficultyBeatmap selectedDifficultyBeatmap;

        [HarmonyPostfix]
        public static void Postfix(ref StandardLevelDetailView __instance)
        {
            selectedDifficultyBeatmap = __instance.selectedDifficultyBeatmap;
            if (!GameObject.Find("Wrapper/MenuCore/UI/ScreenSystem/ScreenContainer/MainScreen/LevelSelectionNavigationController/LevelCollectionNavigationController/LevelDetailViewController/LevelDetail/BSMLBackground/SongNotes_EditNoteButton"))
            {
                Plugin.Log.Info("Creating Note Edit Button");
                string bsml = Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SongNotes.UI.NoteMenu.bsml");
                BSMLParser.instance.Parse(bsml, __instance.gameObject, UI.NoteMenu.GetInstance());
            }

        }
    }
}
