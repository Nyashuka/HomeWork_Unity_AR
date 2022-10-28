using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SpawnableManager : MonoBehaviour
{

    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject[] _spawnablePrefabs;
    [SerializeField] private Camera _ARcamera;
    [SerializeField] private PrefabIterator _prefabIterator;

    private List<ARRaycastHit> _raycastHits = new List<ARRaycastHit>();
    private GameObject _spawnedObject;

    private void Start()
    {
        _spawnedObject = null;
    }

    private void Update()
    {
        if (Input.touchCount == 0)
            return;

        RaycastHit hit;
        Ray ray = _ARcamera.ScreenPointToRay(Input.GetTouch(0).position);

        if (_raycastManager.Raycast(Input.GetTouch(0).position, _raycastHits))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && _spawnedObject == null)
            {
                if (Physics.Raycast(ray, out hit))
                    if (hit.collider.gameObject.tag == "Spawnable")
                    {
                        _spawnedObject = hit.collider.gameObject;
                    }
                    else
                    {
                        SpawnPrefab(_raycastHits[0].pose.position);
                    }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Moved && _spawnedObject != null)
            {
                _spawnedObject.transform.position = _raycastHits[0].pose.position;
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _spawnedObject = null;
            }
        }
    }

    private void SpawnPrefab(Vector3 position)
    {
        _spawnedObject = Instantiate(_spawnablePrefabs[_prefabIterator.Number], position, Quaternion.identity);
    }
}
