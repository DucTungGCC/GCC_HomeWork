using System;
using UnityEngine;

public class GizmoDrawer : MonoBehaviour
{
    [SerializeField] private Transform tf;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask layerOption;
    [SerializeField] private GameObject eventWin;
    [SerializeField] private GameObject[] options = new GameObject[3];
    private Transform _selectedBlock = null;
    private Vector3 _mousePos, _lastPosition;
    private bool _onClicked = false;
    private bool[] _visited = new bool[] { false, false, false };
    private float _deltaX = 0, _deltaY = 0;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
        eventWin.SetActive(false);
    }

    private void Update()
    {
        MakeChose();
    }

    void SetGameStatus()
    {
        eventWin.SetActive(isFilled());
    }

    // Di chuyen Point
    private void MakeChose()
    {
        _mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D hit = Physics2D.OverlapPoint(_mousePos, layerMask);

            // Tim diem keo vat pham tai diem nhap chuot
            if (hit != null)
            {
                _deltaX = hit.transform.position.x - _mousePos.x;
                _deltaY = hit.transform.position.y - _mousePos.y;
                _selectedBlock = hit.transform;
                _lastPosition = _selectedBlock.position;
            }
            
            _onClicked = true;
        }

        if (_onClicked)
        {
            if (_selectedBlock != null)
            {
                _selectedBlock.position = new Vector3(_mousePos.x + _deltaX, _mousePos.y + _deltaY, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_selectedBlock != null)
            {
                Choosen(_selectedBlock);
                SetGameStatus();
                _selectedBlock = null;
            }

            _onClicked = false;
        }
    }


    void Choosen(Transform select) // Tac dung di chuyen cac khoi
    {
        //check xem co dang an vao khoi khong
        Collider2D hit = Physics2D.OverlapPoint(select.position, layerOption); // Se fix lai sau = raycast
        if(hit != null)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (hit.transform == options[i].transform)
                {
                    if (!_visited[i])
                    {
                        select.position = options[i].transform.position;
                        _visited[i] = true;
                    }
                    else
                    {
                        select.position = _lastPosition;
                    }
                }
            }
        }
        else
        {
            if (!isValid(select))
            {
                select.position = _lastPosition;
            }
        }
        
        if (select.position != _lastPosition)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (_lastPosition == options[i].transform.position)
                {
                    _visited[i] = false;
                }
            }
        }

    }

    bool isValid(Transform select)
    {
        return !Physics2D.OverlapCircle(select.position, 1f, layerOption);
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(tf.position.x + i, tf.position.y, 0);
            Gizmos.DrawWireCube(pos, new Vector2(1, 1));
        }
    }

    private bool isFilled()
    {
        for (int i = 0; i < 3; i++)
        {
            if(!_visited[i]) return false;
        }
        return true;
    }

    private void OnDisable() // Tranh viec dang keo thi di ra khoi object
    {
        if (_onClicked && _selectedBlock  != null)
        {
            _selectedBlock.position = _lastPosition;
            _selectedBlock = null;
            _onClicked = false;
        }
    }
}
