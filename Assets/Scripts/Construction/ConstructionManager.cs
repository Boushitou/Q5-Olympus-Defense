using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    public static ConstructionManager Instance;

    private bool b_isBuilding;

    private GameObject _currentlySelectedTower;
    private GameObject _currentlySelectedGhost;
    private Vector3 _mousePos;

    private LayerMask _tileLayer;
    private LayerMask _ghostLayer;
    private Camera _cam;

    private Color _canPlace;
    private Color _cantPlace;

    [SerializeField] private int _faith = 200;
    private int _maxFaith = 9999;

    private bool b_canConstruct = false;

    private TextMeshProUGUI _faithTxt;

    [SerializeField] private List<ConstructionButton> _constructionButtonList; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _tileLayer = LayerMask.GetMask("Ground");
        _cam = Camera.main;
        _canPlace = new Color(0.08915094f, 0.9f, 0.0988174f, 0.5019608f); //green transparent
        _cantPlace = new Color(0.9019608f, 0.1213415f, 0.09019604f, 0.5019608f); //red transparent

        _faithTxt = GameObject.Find("FaithCount").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _faithTxt.text = "Faith : " + _faith;
    } 

    public void UpdateMousePosition(Vector3 mousePosition)
    {
        _mousePos = mousePosition;
    }

    public void MoveSelectedTower()
    {
        if (_currentlySelectedGhost != null)
        {
            Ray ray = _cam.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _tileLayer))
            {
                Vector3 tilePos = new Vector3(hitInfo.transform.position.x, 0, hitInfo.transform.position.z);

                _currentlySelectedGhost.transform.position = tilePos;

                if (hitInfo.transform.gameObject.CompareTag("Constructible"))
                {
                    ChangeColorInChildren(_canPlace);
                }
                else
                {
                    ChangeColorInChildren(_cantPlace);
                }
            }
        }
    }

    private void ChangeColorInChildren(Color color)
    {
        foreach (var renderer in _currentlySelectedGhost.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material.color = color;
        }
    }

    public void PlaceSelectedTower()
    {
        if (_currentlySelectedGhost != null)
        {
            Ray ray = _cam.ScreenPointToRay(_mousePos);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, _tileLayer, ~_ghostLayer))
            {
                if (hitInfo.transform.gameObject.CompareTag("Constructible") && b_canConstruct)
                {
                    Vector3 tilePos = new Vector3(hitInfo.transform.position.x, 0, hitInfo.transform.position.z);

                    GameObject tile = Instantiate(_currentlySelectedTower, tilePos, Quaternion.identity);
                    tile.GetComponent<Tower>().SetTile(hitInfo.transform.gameObject);

                    _currentlySelectedTower.GetComponent<Tower>().InitializeValue();
                    RemoveFaith(_currentlySelectedTower.GetComponent<Tower>().GetCost());
                    UpdateConstructionButton();

                    UnselectTower();

                    hitInfo.transform.gameObject.tag = "Unconstructible";

                    b_canConstruct = false;
                }
            }
        }
    }

    private void UpdateConstructionButton()
    {
        for (int i = 0; i < _constructionButtonList.Count; i++)
        {
            _constructionButtonList[i].UpdateInteractable();
        }
    }

    public void UnselectTower()
    {
        if (_currentlySelectedGhost != null)
        {
            Destroy(_currentlySelectedGhost);
            _currentlySelectedGhost = null;
            _currentlySelectedTower = null;

            b_isBuilding = false;
            b_canConstruct = false;
        }
    }

    public void SetCurrentlySelectedGhost(GameObject towerGhost)
    {
        if (b_isBuilding && _currentlySelectedGhost != towerGhost)
        {
            Debug.Log("destroy ghost");
            Destroy(_currentlySelectedGhost);
        }

        _currentlySelectedGhost = Instantiate(towerGhost);
        b_isBuilding = true;

        UIManager.Instance.CloseConstructionMenu();

        StartCoroutine(ConstructTimer());
    }

    public void SetCurrentlySelectedTower(GameObject tower)
    {
        if (CanBeSelected(tower.GetComponent<Tower>()))
        {
            _currentlySelectedTower = tower;
        }
        else
        {
            UnselectTower();
        }
    }

    public void AddFaith(int value)
    {
        _faith += _faith >= _maxFaith ? 0 : value;
        

        _faithTxt.text = "Faith : " + _faith;
    }

    public void RemoveFaith(int value)
    {
        _faith -= _faith <= 0 ? 0 : value;

        _faithTxt.text = "Faith : " + _faith;
    }

    public int GetFaith() { return _faith; }

    public bool CanBeSelected(Tower tower)
    {
        if (tower.GetCost() <= _faith)
        {
            return true;
        }

        return false;
    }

    public void SetCanConstruct(bool canConstruct)
    {
        b_canConstruct = canConstruct;
    }

    private IEnumerator ConstructTimer()
    {
        yield return new WaitWhile(() => Input.GetMouseButtonUp(0));

        b_canConstruct = true;
    }
}
