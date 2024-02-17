using UnityEditor;

namespace MaximovInk.AdvancedTilemap
{

    [CustomEditor(typeof(ATileset))]
    public class ATilesetEditor : Editor
    {
        private ATileset _tileset;
        private ATilesetEditorData _data;

        private void OnEnable()
        {
            _tileset = (ATileset)target;
        }

        public override void OnInspectorGUI()
        {
            if (ATilesetGUI.DrawGUI(_tileset, ref _data))
            {
                EditorUtility.SetDirty(_tileset);
            }
        }
    }
}