using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Cursor : MonoBehaviour
{
    public GameMap Map;
    public Point Position;
    public GameObject Selector;

    public Material TrueMaterial;
    public Material FalseMaterial;

    private MeshRenderer[,] _aim;
    private ConstructionBase _building;
    private bool _canBuild = false;

    private List<ConstructionBase> _transpConstr = new List<ConstructionBase>();

    void Start()
    {
        var input = GetComponent<InputController>();
        input["Fire1"].FirstClick += LeftClick;
        input["Cancel"].FirstClick += CancelTask;


        var aimObject = (new GameObject("Aim")).transform;
        aimObject.SetParent(transform);
        _aim = new MeshRenderer[3, 3];
        var aimPrefab = Resources.Load<MeshRenderer>("Prefabs/Aim");
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                var aim = Instantiate<MeshRenderer>(aimPrefab);
                aim.transform.SetParent(aimObject);
                aim.transform.localPosition = new Vector3(x + 0.5f, 0.1f, y + 0.5f);
                aim.enabled = false;
                _aim[x, y] = aim;
            }
    }

    public void BeginPlace(ConstructionBase building)
    {
        if (_building != null) Destroy(_building.gameObject);
        _building = building;
        building.transform.SetParent(transform);
        building.transform.localPosition = Vector3.zero;
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                _aim[x, y].enabled = x < building.Size.X && y < building.Size.Y;
            }
    }

    void LeftClick()
    {
        if (_canBuild && !EventSystem.current.IsPointerOverGameObject() && _building != null)
        {
            Map.PlaceBuilding(_building, Position);
            ClearTask();
        }
    }

    void CancelTask()
    {
        if (_building != null)
        {
            Destroy(_building.gameObject);
            ClearTask();
        }
    }

    void ClearTask()
    {
        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
            {
                _aim[x, y].enabled = false;
            }
        _building = null;
    }

    private Vector3 _normal = new Vector3(0, 1, 0);
    void Update()
    {
        if (_building != null)
        {
            Point size = _building.Size;
            Vector3 mousePos = Input.mousePosition;
            var ray = Camera.main.ScreenPointToRay(new Vector3(mousePos.x, mousePos.y, 1f));

            Plane plane = new Plane(_normal, new Vector3(0f, 0f, 0f));
            float distance;
            plane.Raycast(ray, out distance);
            Vector3 point = ray.GetPoint(distance);
            Position = new Point(Mathf.FloorToInt(point.x - size.X / 2f + 0.5f), Mathf.FloorToInt(point.z - size.Y / 2f + 0.5f));
            Position = new Point(
                Position.X < 0 ? 0 : Position.X > Map.Width - size.X ? Map.Width - size.X : Position.X,
                Position.Y < 0 ? 0 : Position.Y > Map.Height - size.Y ? Map.Height - size.Y : Position.Y);
            transform.position = Position;

            foreach (var constr in _transpConstr)
                constr.MakeVisible(true);
            _transpConstr.Clear();

            _canBuild = true;
            for (int x = 0; x < size.X; x++)
                for (int y = 0; y < size.Y; y++)
                {
                    var cell = Map[Position + new Point(x, y)];
                    _canBuild = _canBuild && cell.CanBuild;
                    _aim[x, y].material = cell.CanBuild ? TrueMaterial : FalseMaterial;
                    if (cell.Construction != null && !_transpConstr.Contains(cell.Construction))
                    {
                        _transpConstr.Add(cell.Construction);
                        cell.Construction.MakeVisible(false);
                    }
                }

        }
    }
}
