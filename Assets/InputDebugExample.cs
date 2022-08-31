using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebugExample : MonoBehaviour
{
    public InputAction singlePlayerAction;
    public float moveSpeed = 10f;
    public Vector2 position;

    private InputAction aButton;


    // Start is called before the first frame update
    void Start()
    {
        singlePlayerAction.Enable();

        // Set up an action that triggers when the A button on
        // the gamepad is released.
        aButton = new InputAction(
            type: InputActionType.Button,
            binding: "<Gamepad>/buttonSouth",
            interactions: "press(behavior=1)");
        aButton.AddBinding("<Mouse>/leftButton");

        aButton.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        var moveDirection = singlePlayerAction.ReadValue<Vector2>();
        position += moveDirection * moveSpeed * Time.deltaTime;
        Debug.Log(position);

        if (aButton.triggered)
            Debug.Log("A button on gamepad was released this frame");
    }


    public void OnSelect()
    {
        Debug.Log("Select");
    }

}
