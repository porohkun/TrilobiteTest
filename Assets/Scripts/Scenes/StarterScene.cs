using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StarterScene : SceneBase
{
    void Start()
    {
        var map = Instantiate<GameMap>(Global.MapPrefab);
        map.Init(Global.MapSize.X, Global.MapSize.Y);
        ScenesManager.Instance.Push(GameScene.GetInstance(map));
    }
}
