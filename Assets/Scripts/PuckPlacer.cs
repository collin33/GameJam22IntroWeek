
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

    private readonly Vector3 _cameraStartPosition = new Vector3(11, 2f, 0.1f);
    private readonly Vector3 _cameraLookAt = new Vector3(25f, -90f, 0f);
    
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
        _cam.transform.rotation =  UnityEngine.Quaternion.Euler(_cameraLookAt) ;
    }

    private void Update()
    {
        var moveDirection = moveAction.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime);

        if (_isPlacing && currentPuck)
        {
            //STATE A: GENERAL VIEW, ALLOW FOR PLACING
            //Debug.Log("State A");

            currentPuck.velocity = new Vector3(-moveDirection.y, currentPuck.velocity.y, moveDirection.x);

        } else if (_isShooting && currentPuck)
        {
            // STATE B: CAMERA ADJUSTED, ALLOW FOR AIMING
            //Debug.Log("State B");

            var puckTransForm = currentPuck.transform;

            Vector3 position = new Vector3();
            position = puckTransForm.position + new Vector3(1.5f, 1, 0);

            _cam.transform.position = position;
            _cam.transform.rotation =  UnityEngine.Quaternion.Euler(_cameraLookAt) ;

            if (enterAction.triggered){
                currentPuck.velocity=new Vector3(-transform.position.z,0,0)*20;
            }

        }
        else
        {
            //SAFE SCENARIO, DEBUGS IN CASE SOMETHING UNINTENDED HAPPENS
            Debug.Log(_isPlacing);
            Debug.Log(currentPuck);
        }

        if (enterAction.triggered && _isPlacing && !_isShooting)
        {
            //STATE UPDATER, CHANGE FROM STATE A TO B
            _isPlacing = false;
            _isShooting = true;
            currentPuck.velocity=new Vector3(0,0,0);
        }

        if (backAction.triggered && _isShooting && !_isPlacing)
        {
            //STATE UPDATER, REVERT FROM STATE B TO A
            _isPlacing = true;
            _isShooting = false;

            _cam.transform.position = _cameraStartPosition;
            _cam.transform.rotation =  UnityEngine.Quaternion.Euler(_cameraLookAt) ;
        }

    }
}