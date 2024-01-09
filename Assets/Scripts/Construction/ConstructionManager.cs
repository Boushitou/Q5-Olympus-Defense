using UnityEditor.SceneManagement;
using UnityEngine;

public class ConstructionManager : MonoBehaviour
{
    private bool b_isBuilding;

    private GameObject _currentlySelected;
    private Vector3 _mousePos;

    private LayerMask _tileLayer;
    private LayerMask _ghostLayer;
    private Camera _cam;

    private void Awake()
    {
        _tileLayer = LayerMask.GetMask("Ground");
        _cam = Camera.main;
    }

    private void Update()
    {
        if (_currentlySelected != null)
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
            Vector3 tilePos = hitInfo.transform.position;

            _currentlySelected.transform.position = tilePos;
        }
    }

    public void PlaceSelectedTower(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo, _tileLayer, ~_ghostLayer))
        {
            if (hitInfo.transform.gameObject.CompareTag("Constructible"))
            {
                Vector3 tilePos = hitInfo.transform.position;
                Instantiate(_currentlySelected, tilePos, Quaternion.identity);

                hitInfo.transform.gameObject.tag = "Unconstructible";
            }
        }
    }

    public void UnselectTower()
    {
        Destroy(_currentlySelected);
        _currentlySelected = null;

        b_isBuilding = false;
    }

    public void SetCurrentlySelectedTower(GameObject tower)
    {
        if (b_isBuilding && _currentlySelected != tower)
        {
            Debug.Log("destroy ghost");
            Destroy(_currentlySelected);
        }

        _currentlySelected = Instantiate(tower);
        b_isBuilding = true;
    }
}
