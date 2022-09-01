
using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PuckPlacer : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction enterAction;
    public InputAction backAction;
    
    public Rigidbody currentPuck;
    [SerializeField] private float moveSpeed = 50;

    private readonly Vector3 _cameraStartPosition = new Vector3(11, 1.5f, 0.1f);
    private readonly Vector3 _cameraLookAt = new Vector3(-9.972f, 0.1885f, 0.0234f);
    
    private bool _isPlacing = true;
    private bool _isShooting;

    private Camera _cam;
    
    
    private void Start()
    {
        _cam = Camera.main;
        moveAction.Enable();
        enterAction.Enable();
        backAction.Enable();
        
        _cam.transform.position = _cameraStartPosition;
        _cam.transform.LookAt(_cameraLookAt);
    }

    private void Update()
    {
        var moveDirection = moveAction.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime);

        if (_isPlacing && currentPuck)
        {
            currentPuck.velocity = new Vector3(-moveDirection.y, currentPuck.velocity.y, moveDirection.x);
        } else if (_isShooting && currentPuck)
        {
            var puckTransForm = currentPuck.transform;

            Vector3 position = new Vector3();
            position = puckTransForm.forward + puckTransForm.position + new Vector3(0, 1, 0);

            _cam.transform.position = position;
            _cam.transform.LookAt(_cameraLookAt);
        }
        else
        {
            Debug.Log(_isPlacing);
            Debug.Log(currentPuck);
        }

        if (enterAction.triggered && _isPlacing && !_isShooting)
        {
            _isPlacing = false;
            _isShooting = true;
        }

        if (backAction.triggered && _isShooting && !_isPlacing)
        {
            _isPlacing = true;
            _isShooting = false;

            _cam.transform.position = _cameraStartPosition;
            _cam.transform.LookAt(_cameraLookAt);
        }

    }
}