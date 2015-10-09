using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Global
{
    public static GameMap MapPrefab = Resources.Load<GameMap>("Prefabs/Map");
    public static float TextureStep { get; private set; }
    public static float PartSize { get; private set; }
    public static Point MapSize { get; private set; }


    private static Dictionary<string, Vector2> _materialsTex = new Dictionary<string, Vector2>();
    
    public static void LoadSettings(Settings settings)
    {
        TextureStep = settings.TextureStep;
        PartSize = settings.PartTextureSize;
        foreach (var tp in settings.Materials)
        {
            _materialsTex.Add(tp.Name, tp.TexCoord * TextureStep);
        }
        MapSize = settings.MapSize;
    }

    public static Vector2 GetTexture(string name)
    {
        return _materialsTex[name];
    }
}