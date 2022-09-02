
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

    private readonly Vector3 _cameraStartPosition = new Vector3(10, 1, 0);
    private readonly Vector3 _cameraLookAt = new Vector3(0, 0, 15);
    
    private bool _isPlacing = true;
    private bool _isShooting;

    public Transform _cam;
    
    
    private void Start()
    {
        moveAction.Enable();
        enterAction.Enable();
        backAction.Enable();
        
        _cam.position = _cameraStartPosition;
        _cam.transform.rotation =  UnityEngine.Quaternion.Euler(_cameraLookAt);
    }

    private void Update()
    {

        if (_isPlacing && currentPuck)
        {
            //STATE A: GENERAL VIEW, ALLOW FOR PLACING
            //Debug.Log("State A");
            var moveDirection = moveAction.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime);
            currentPuck.velocity = new Vector3(-moveDirection.y, currentPuck.velocity.y, moveDirection.x);

        } else if (_isShooting && currentPuck)
        {
            // STATE B: CAMERA ADJUSTED, ALLOW FOR AIMING
            //Debug.Log("State B");

            var puckTransForm = currentPuck.transform;

            Vector3 position = new Vector3();
            position = puckTransForm.position;

            _cam.position = position;

            if (enterAction.triggered){
                currentPuck.velocity=new Vector3(-transform.position.z,0,0)*80;
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
            _cam.transform.rotation =  UnityEngine.Quaternion.Euler(0,0,0);
        }

        if (backAction.triggered && _isShooting && !_isPlacing)
        {
            //STATE UPDATER, REVERT FROM STATE B TO A
            _isPlacing = true;
            _isShooting = false;

            _cam.position = _cameraStartPosition;
        }

    }
}