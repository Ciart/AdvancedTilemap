﻿

using System.Collections.Generic;
using UnityEngine;

namespace Ciart.AdvancedTilemap
{
    [System.Serializable]
    public class ATilemapCommand
    {

        [System.Serializable]
        public class TileData
        {
            public ushort OldTileData;
            public ushort NewTileData;
            public Vector2Int Position = Vector2Int.zero;
        }

        public List<TileData> tileChanges = new List<TileData>();

        public bool isEmpty()
        {
            return tileChanges.Count == 0;
        }
    }

    [System.Serializable]
    public class TilemapCommandContainer
    {
        public Stack<ATilemapCommand> undoCommands = new Stack<ATilemapCommand>();
        public Stack<ATilemapCommand> redoCommands = new Stack<ATilemapCommand>();
    }
}
