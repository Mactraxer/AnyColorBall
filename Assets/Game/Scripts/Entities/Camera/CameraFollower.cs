using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Camera _camera;

    public void Follow(GameObject player)
    {
        _target = player.transform;
    }

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (_target == null) return;
        
        _camera.transform.position = new Vector3(_target.position.x, _target.position.y, _camera.transform.position.z);
    }
}
