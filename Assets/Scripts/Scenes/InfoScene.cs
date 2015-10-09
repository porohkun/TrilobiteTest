using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoScene : SceneBase
{
    private ConstructionBase _construction;
    public Text Header;
    public Button DeleteButton;
    public override bool Transparent { get { return true; } }

    public static InfoScene GetInstance(ConstructionBase construction)
    {
        var scene = Instantiate<InfoScene>();
        scene._construction = construction;
        scene.Header.text = construction.GetType().Name;
        scene.DeleteButton.interactable = construction.Removable;
        return scene;
    }

    public void OnSize()
    {
        ScenesManager.Instance.Push(SizeScene.GetInstance(_construction));
    }

    public void OnDelete()
    {
        _construction.Remove();
        OnBack();
    }

    public void OnBack()
    {
        ScenesManager.Instance.Pop();
    }
}
