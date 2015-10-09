using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    public TexturePointer[] Materials;
    public float TextureStep = 1f;
    public float PartTextureSize = 4f / 24f;
    public Point MapSize;

    void Start()
    {
        Global.LoadSettings(this);
    }


    [Serializable]
    public struct TexturePointer
    {
        public string Name;
        public Point TexCoord;
    }
}
