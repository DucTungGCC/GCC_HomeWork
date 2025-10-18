using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GizmoDrawer : MonoBehaviour
{
    [SerializeField] private Transform _tf;
    [SerializeField] private Camera _cam;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private LayerMask _layerOption;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private GameObject eventWin;
    [SerializeField] private GameObject[] Options = new GameObject[3];
    public int count = 0;
    private Transform selectBlock = null;
    private Vector3 _mousePos;
    private Vector3 _lastPosition;
    private bool miniStatus = false;
    bool[] visit = new bool[3] { false, false, false };
    private float deltaX = 0, deltaY = 0;
    private void Awake()
    {
        if(_cam == null)
            _cam = Camera.main;
    }

    private void Update()
    {
        MakeChose();
        if(miniStatus)
            eventWin.SetActive(true);
        else eventWin.SetActive(false);
        if (count == 3)
        {
            eventWin.SetActive(true);
        }
        else
        {
            eventWin.SetActive(false);
        }
    }

    private void MakeChose()
    {
        _mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);
        if (_inputManager.inputState == InputManager.InputState.StartClick)
        {
            Collider2D hit = Physics2D.OverlapPoint(_mousePos, _layerMask);
            if (hit != null)
            {
                deltaX = hit.transform.position.x - _mousePos.x;
                deltaY = hit.transform.position.y - _mousePos.y;
                selectBlock = hit.transform;
                _lastPosition = selectBlock.position;   
            }
        }

        if (_inputManager.inputState == InputManager.InputState.OnClick)
        {
            if (selectBlock != null)
            {
                selectBlock.position = new Vector3(_mousePos.x + deltaX, _mousePos.y + deltaY, 0);
            }
        }

        if (_inputManager.inputState == InputManager.InputState.EndClick)
        {
            if (selectBlock != null)
            {
                Choosen(selectBlock);
                selectBlock = null;
            }
        }
    }
    
    
    void Choosen(Transform select)
    {
        Collider2D hit = Physics2D.OverlapPoint(select.position, _layerOption);
        if (hit != null)
        {
            if (hit.transform == Options[0].transform && visit[0] == false)
            {
                select.position = Options[0].transform.position;
                Debug.Log("Option selected 1");
                visit[0] = true;
                count++;
            }
            else if (hit.transform == Options[0].transform && visit[0])
            {
                select.position = _lastPosition;
            }
            if (hit.transform == Options[1].transform && visit[1] == false)
            {
                select.position = Options[1].transform.position;
                Debug.Log("Option selected 2");
                visit[1] = true;
                count++;
            }
            else if (hit.transform == Options[1].transform && visit[1])
            {
                 select.position = _lastPosition;
            }
            if(hit.transform == Options[2].transform && visit[2] == false)
            {
                select.position = Options[2].transform.position;
                Debug.Log("Option selected 3");
                visit[2] = true;
                count++;
            }
            else if (hit.transform == Options[2].transform && visit[2])
            {
                select.position = _lastPosition;
            }
        }
        else
        {
            if (nonValid(select))
            {
                select.position = _lastPosition;
            }
        }

        if (select.position != _lastPosition)
        {
            if (_lastPosition == Options[0].transform.position)
            {
                visit[0] = false;
                count--;
            }
            if (_lastPosition == Options[1].transform.position)
            {
                visit[1] = false;
                count--;
            }
            if (_lastPosition == Options[2].transform.position)
            {
                visit[2] = false;
                count--;
            }
        }
        
    }

    bool nonValid(Transform select)
    {
        return Physics2D.OverlapCircle(select.position, 1f, _layerOption);
    }
    
    private void OnDrawGizmos()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 pos = new Vector3(_tf.position.x + i,  _tf.position.y , 0);
            Gizmos.DrawWireCube(pos , new Vector2(1, 1));
        }
    }
}
