using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    private bool b_isBuilding;

    private GameObject _currentlySelectedTower;
    private GameObject _currentlySelectedGhost;
    private Vector3 _mousePos;

    private LayerMask _tileLayer;
    private LayerMask _ghostLayer;
    private Camera _cam;

    private Color _canPlace;
    private Color _cantPlace;

    private void Awake()
    {
        _tileLayer = LayerMask.GetMask("Ground");
        _cam = Camera.main;
        _canPlace = new Color(0.08915094f, 0.9f, 0.0988174f, 0.5019608f); //green transparent
        _cantPlace = new Color(0.9019608f, 0.1213415f, 0.09019604f, 0.5019608f); //red transparent
    }

    private void Update()
    {
        if (_currentlySelectedGhost != null)
        {
            _mousePos = Input.mousePosition;

            Ray ray = _cam.ScreenPointToRay(_mousePos);

            MoveSelectedTower(ray);

            if (Input.GetMouseButtonUp(0))
            {
                PlaceSelectedTower(ray);
            }
            else if (Input.GetMouseButtonUp(1))
            {
                UnselectTower();
            }
        }
    }

    public void MoveSelectedTower(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _tileLayer))
        {
            Vector3 tilePos = new Vector3(hitInfo.transform.position.x, 0, hitInfo.transform.position.z);

            _currentlySelectedGhost.transform.position = tilePos;

            if (hitInfo.transform.gameObject.CompareTag("Constructible"))
            {
                _currentlySelectedGhost.GetComponent<MeshRenderer>().material.color = _canPlace;
            }
            else
            {
                _currentlySelectedGhost.GetComponent<MeshRenderer>().material.color = _cantPlace;
            }
        }
    }

    public void PlaceSelectedTower(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _tileLayer, ~_ghostLayer))
        {
            if (hitInfo.transform.gameObject.CompareTag("Constructible"))
            {
                Vector3 tilePos = new Vector3(hitInfo.transform.position.x, 0, hitInfo.transform.position.z);

                Instantiate(_currentlySelectedTower, tilePos, Quaternion.identity);
                UnselectTower();

                hitInfo.transform.gameObject.tag = "Unconstructible";
            }
        }
    }

    public void UnselectTower()
    {
        Destroy(_currentlySelectedGhost);
        _currentlySelectedGhost = null;
        _currentlySelectedTower = null;

        b_isBuilding = false;
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
    }

    public void SetCurrentlySelectedTower(GameObject tower)
    {
        _currentlySelectedTower = tower;
    }
}
