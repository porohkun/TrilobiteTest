using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameScene : SceneBase
{
    public float PanelSpeed = 200f;
    public RectTransform ButtonPanel;
    private GameMap _map;
    private Dictionary<string, ConstructionBase> _buildings = new Dictionary<string, ConstructionBase>();

    private bool _moving = false;
    private bool _open = false;

    public static GameScene GetInstance(GameMap map)
    {
        var scene = Instantiate<GameScene>();
        scene._map = map;
        scene._buildings.Add("building1x1", Resources.Load<ConstructionBase>("Prefabs/Building1x1"));
        scene._buildings.Add("building2x2", Resources.Load<ConstructionBase>("Prefabs/Building2x2"));
        scene._buildings.Add("building3x3", Resources.Load<ConstructionBase>("Prefabs/Building3x3"));
        return scene;
    }

    public void OnBuildButton()
    {
        if (_open)
            Close();
        else
            Open();
    }

    public void On1x1Button()
    {
        Close();
        _map.Cursor.BeginPlace(Instantiate<ConstructionBase>(_buildings["building1x1"]));
    }

    public void On2x2Button()
    {
        Close();
        _map.Cursor.BeginPlace(Instantiate<ConstructionBase>(_buildings["building2x2"]));
    }

    public void On3x3Button()
    {
        Close();
        _map.Cursor.BeginPlace(Instantiate<ConstructionBase>(_buildings["building3x3"]));
    }

    void Open()
    {
        _open = true;
        _moving = true;
    }

    void Close()
    {
        _open = false;
        _moving = true;
    }

    void Update()
    {
        if (_moving)
        {
            float x = ButtonPanel.anchoredPosition.x;
            if (_open)
            {
                x += PanelSpeed * Time.deltaTime;
                if (x >= 0f)
                {
                    x = 0f;
                    _moving = false;
                }
            }
            else
            {
                x -= PanelSpeed * Time.deltaTime;
                if (x <= -ButtonPanel.sizeDelta.x)
                {
                    x = -ButtonPanel.sizeDelta.x;
                    _moving = false;
                }
            }
            ButtonPanel.anchoredPosition = new Vector2(x, 0f);
        }
    }
}