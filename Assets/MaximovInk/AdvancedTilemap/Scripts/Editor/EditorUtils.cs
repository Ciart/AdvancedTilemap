﻿using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MaximovInk.AdvancedTilemap
{
    public class EditorUtils
    {
        public static void DrawRect(Rect rect, Color color)
        {
            DrawRectWithOutline(rect, color, color);
        }

        public static void DrawRectWithOutline(Rect rect, Color color, Color colorOutline)
        {
            Vector3[] rectVerts = {
            new Vector3(rect.x, rect.y, 0),
            new Vector3(rect.x + rect.width, rect.y, 0),
            new Vector3(rect.x + rect.width, rect.y + rect.height, 0),
            new Vector3(rect.x, rect.y + rect.height, 0) };
            Handles.DrawSolidRectangleWithOutline(rectVerts, color, colorOutline);
        }

        public static void DrawPreviewTileset(ATileset tileset, ref ushort selectedTile, ref float previewScaler, ref Vector2 scrollViewValue)
        {
            if (tileset == null || tileset.Texture == null) return;

            previewScaler = EditorGUILayout.Slider("Scale:", previewScaler, 0.2f, 10f);

            scrollViewValue = GUILayout.BeginScrollView(scrollViewValue, "helpBox", GUILayout.Height(300));



            var aspect = (float)tileset.Texture.height / tileset.Texture.width;

            int scaledValue = (int)(200 * previewScaler * 1.45f);

            GUILayout.Label("");
            var rectW = GUILayoutUtility.GetLastRect();


            GUILayout.Label("", GUILayout.Width(scaledValue / aspect), GUILayout.Height(scaledValue));

            var rect = GUILayoutUtility.GetLastRect();

            rect.x += rectW.width / 2f - rect.width / 2f;

            DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1f));

            GUI.DrawTexture(rect, tileset.Texture);

            for (int i = 0; i < tileset.TilesCount; i++)
            {
                var tile = tileset.GetTile(i);

                if (i == selectedTile)
                    DrawPreviewTile(rect, tile, new Color(1, 1, 1, 0.5f), new Color(0, 0, 0, 0.2f), ref selectedTile);

                else
                    DrawPreviewTile(rect, tile, new Color(1, 1, 1, 0.5f), Color.clear, ref selectedTile);
            }

            if (selectedTile > 0 && selectedTile < tileset.TilesCount+1)
            {
                var tile = tileset.GetTile(selectedTile);

                DrawPreviewTile(rect, tile, Color.red, Color.clear, ref selectedTile);

                for (int i = 1; i < tile.Variations.Count; i++)
                {
                    DrawPreviewTile(rect, tile.Variations[i], new Color(0, 0, 1, 0.5f), Color.clear);
                }
            }

            GUILayout.EndScrollView();
        }

        public static void DrawPreviewTile(Rect rect, ATileUV uv, Color color, Color fillColor)
        {
            var uvSize = uv.Max - uv.Min;

            var tileRect = new Rect(
                (int)(rect.x + uv.Min.x * rect.width),
                (int)(rect.y + rect.height - uv.Max.y * rect.height),
                (int)(uvSize.x * rect.width),
                (int)(uvSize.y * rect.height)
                );

            DrawRectWithOutline(tileRect, fillColor, color);
        }

        public static void DrawPreviewTile(Rect rect, ATile tile, Color color, Color fillColor, ref ushort selectedTile)
        {

            var uv = tile.GetUV();
            var uvSize = uv.Max - uv.Min;

            var tileRect = new Rect(
                (int)(rect.x + uv.Min.x * rect.width),
                (int)(rect.y + rect.height - uv.Max.y * rect.height),
                (int)(uvSize.x * rect.width),
                (int)(uvSize.y * rect.height)
                );

            if (GUI.Button(tileRect, "", GUIStyle.none))
            {
                if (selectedTile == tile.ID)
                    selectedTile = 0;
                else
                    selectedTile = tile.ID;
            }

            DrawRectWithOutline(tileRect, fillColor, color);
        }

        public static void SaveAsset(Object obj)
        {
            EditorUtility.SetDirty(obj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
