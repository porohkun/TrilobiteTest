using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

    public class CounstructionHolder:MonoBehaviour
    {
        public ConstructionBase Construction;

        public void OnMouseDown()
        {
            if (Construction.Cell != null && !EventSystem.current.IsPointerOverGameObject())
                ScenesManager.Instance.Push(InfoScene.GetInstance(Construction));
        }
    }
