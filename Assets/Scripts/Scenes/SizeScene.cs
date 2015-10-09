using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SizeScene : SceneBase
{
    private ConstructionBase _construction;
    public Text Size;
    public override bool Transparent { get { return true; } }
    public static SizeScene GetInstance(ConstructionBase construction)
    {
        var scene = Instantiate<SizeScene>();
        scene._construction = construction;
        scene.Size.text = construction.Size.ToString();
        return scene;
    }

    public void OnBack()
    {
        ScenesManager.Instance.Pop();
    }
}
