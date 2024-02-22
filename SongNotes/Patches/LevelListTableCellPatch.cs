using HarmonyLib;
using HMUI;
using IPA.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SongNotes.Patches
{
    [HarmonyPatch(typeof(LevelListTableCell), nameof(LevelListTableCell.SetDataFromLevelAsync))]
    internal class LevelListTableCellPatch
    {
        static HoverHintController _hoverHintController;
        internal static Dictionary<string, HoverHint> hoverHints = new Dictionary<string, HoverHint>();

        [HarmonyPostfix]
        public static void LevelListTableCell_SetDataFromLevelAsync(ref LevelListTableCell __instance, IPreviewBeatmapLevel level)
        {
            if (_hoverHintController == null)
                _hoverHintController = Resources.FindObjectsOfTypeAll<HoverHintController>().FirstOrDefault();

            if (!__instance.gameObject.GetComponent<HoverHint>())
                __instance.gameObject.AddComponent<HoverHint>();

            HoverHint hint = __instance.gameObject.GetComponent<HoverHint>();
            hint.text = Configuration.GetInstance().GetNoteForSong(level.levelID);
            hint.SetField("_hoverHintController", _hoverHintController);

            if (hoverHints.ContainsKey(level.levelID))
                hoverHints[level.levelID] = hint;
            else
                hoverHints.Add(level.levelID, hint);
        }
    }
}
