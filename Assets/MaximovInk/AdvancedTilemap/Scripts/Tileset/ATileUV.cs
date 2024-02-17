﻿using UnityEngine;

namespace MaximovInk.AdvancedTilemap
{
    [System.Serializable]
    public struct ATileUV
    {
        public Vector2 LeftTop;
        public Vector2 RightTop;
        public Vector2 RightBottom;
        public Vector2 LeftBottom;

        public override string ToString()
        {
            return $"{LeftBottom.x} {LeftBottom.y} {RightTop.x} {RightTop.y}";
        }

        public Vector2 Min
        {
            get => LeftBottom;
            set
            {
                LeftBottom = value;
                LeftTop.x = value.x;
                RightBottom.y = value.y;
            }
        }

        public Vector2 Size => new Vector2(RightTop.x - LeftBottom.x, RightTop.y - LeftBottom.y);

        public Vector2 Max
        {
            get => RightTop;
            set
            {
                RightTop = value;
                LeftTop.y = value.y;
                RightBottom.x = value.x;
            }
        }
        public static ATileUV Generate(Vector2 min,Vector2 max)
        {
            ATileUV uv = default(ATileUV);
            uv.Min = min;
            uv.Max = max;
            return uv;
        }  
        public static ATileUV Identity
        {
            get
            {
                ATileUV uv;
                uv.LeftBottom = Vector2.zero;
                uv.LeftTop = new Vector2(0f, 1f);
                uv.RightBottom = new Vector2(1f, 0f);
                uv.RightTop = new Vector2(1f, 1f);
                return uv;
            }
        }

    }
}