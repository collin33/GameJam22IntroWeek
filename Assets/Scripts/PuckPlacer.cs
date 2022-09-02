
using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class PuckPlacer : MonoBehaviour
{
    public PuckSpawner PuckSpawn;
    public InputAction moveAction;
    public InputAction enterAction;
    public InputAction backAction;

    

    public Rigidbody currentPuck;
    [SerializeField] private float moveSpeed = 50;

    private readonly Vector3 _cameraStartPosition = new Vector3(10, 1, 0);
    private readonly Vector3 _cameraLookAt = new Vector3(0, 0, 15);

    private bool _isPlacing = true;
    private bool _isShooting;

    private float camRotation;

    public Transform _cam;
    float CameraTimer = 0;
    bool cutscene = false;

    [Header("Camera shit")]
    public PuckSpawner spawner;
    private void Start()
    {
        moveAction.Enable();
        enterAction.Enable();
        backAction.Enable();

        //PuckSpawn.Spawn();
        currentPuck = PuckSpawn.Spawn().GetComponent<Rigidbody>();

        _cam.position = _cameraStartPosition;
        _cam.transform.rotation = UnityEngine.Quaternion.Euler(_cameraLookAt);
    }

    private void Update()
    {
        if (cutscene == true)
        {
            if (_isPlacing == false && CameraTimer > 0)
            {
                CameraTimer -= Time.deltaTime;
                Debug.Log("AAAAAAAAAAAA");
            }
            else if (CameraTimer < 0)
            {
                if (PuckSpawn.remaining <= PuckSpawn.pucks)
                {
                    if (spawner.remaining <= 0)
                        SceneManager.LoadScene("MENU");
                    currentPuck = PuckSpawn.Spawn().GetComponent<Rigidbody>();
                }
                _isShooting = false;
                _isPlacing = true;
                cutscene = false;
            }
        }

        if (_isPlacing && currentPuck && !cutscene)
        {
            //STATE A: GENERAL VIEW, ALLOW FOR PLACING
            //Debug.Log("State A");
            var moveDirection = moveAction.ReadValue<Vector2>() * (moveSpeed * Time.deltaTime);
            currentPuck.velocity = new Vector3(-moveDirection.y, currentPuck.velocity.y, moveDirection.x);
        }
        else if (_isShooting && currentPuck && !cutscene)
        {
            // STATE B: CAMERA ADJUSTED, ALLOW FOR AIMING
            //Debug.Log("State B");

            var puckTransForm = currentPuck.transform;

            Vector3 position = new Vector3();
            position = puckTransForm.position;

            _cam.position = position;

            camRotation += moveAction.ReadValue<Vector2>().x * (moveSpeed * Time.deltaTime);
            currentPuck.rotation = UnityEngine.Quaternion.Euler(currentPuck.rotation.x, camRotation - 90, currentPuck.rotation.z);

            _cam.rotation = UnityEngine.Quaternion.Euler(0, camRotation, 0);

            if (enterAction.triggered)
            {
                currentPuck.AddForce(currentPuck.transform.forward * (21 * moveSpeed));
                _cam.position = _cameraStartPosition;
                _cam.transform.rotation = UnityEngine.Quaternion.Euler(_cameraLookAt);
                cutscene = true;
                CameraTimer = 5;
                /*
                if (PuckSpawn.remaining <= PuckSpawn.pucks)
                {
                    currentPuck = PuckSpawn.Spawn().GetComponent<Rigidbody>();
                }
                _isShooting = false;
                _isPlacing = true;*/
            }
        }
        else
        {
            //SAFE SCENARIO, DEBUGS IN CASE SOMETHING UNINTENDED HAPPENS
            Debug.Log(_isPlacing);
            Debug.Log(currentPuck);
        }

        if (enterAction.triggered && _isPlacing && !_isShooting && !cutscene)
        {
            //STATE UPDATER, CHANGE FROM STATE A TO B
            _isPlacing = false;
            _isShooting = true;
            currentPuck.velocity = new Vector3(0, 0, 0);
            _cam.transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 0);
            camRotation = 0;
        }

        if (backAction.triggered && _isShooting && !_isPlacing && !cutscene)
        {
            //STATE UPDATER, REVERT FROM STATE B TO A
            _isPlacing = true;
            _isShooting = false;
            camRotation = 0;
            _cam.position = _cameraStartPosition;
        }
    }
}