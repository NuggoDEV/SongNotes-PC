using BeatSaberMarkupLanguage.Attributes;
using HMUI;
using SongNotes.Patches;
using UnityEngine.UI;

namespace SongNotes.UI
{
    internal class NoteMenu
    {
        private static NoteMenu _instance;

        public static NoteMenu GetInstance()
        {
            if (_instance == null)
                _instance = new NoteMenu();
            return _instance;
        }

        string noteValue;

        [UIValue("NoteValue")] string _noteVal = "default";
        [UIAction("NoteEntered")] public void NoteEntered(string value) => noteValue = value;


        [UIComponent("NoteEditMenu")]
        private ModalView _noteEditMenu;

        [UIComponent("NoteEditButton")]
        private Button _noteEditButton;

        [UIAction("OpenNoteEditMenu")]
        public void OpenNoteEditMenu() => _noteEditMenu.Show(true);

        [UIAction("CloseNoteEditMenu")]
        public void CloseNoteEditMenu() => _noteEditMenu.Hide(true);

        [UIAction("SaveNote")]
        public void SaveNote()
        {
            string levelID = StandardLevelDetailViewPatch.selectedDifficultyBeatmap.level.levelID;
            
            Configuration.GetInstance().SetNoteForSong(levelID, noteValue);
            LevelListTableCellPatch.hoverHints[levelID].text = noteValue;
            
            _noteEditMenu.Hide(true);
        }

        [UIAction("#post-parse")]
        private void PostParse() => _noteEditButton.gameObject.name = "SongNotes_EditNoteButton";
    }
}
