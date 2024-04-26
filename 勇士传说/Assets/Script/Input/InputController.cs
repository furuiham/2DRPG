using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour {
    private PlayerInputControl playerInputControl;

    public InputController instance;
    public Vector2 inputDirecion;
    public bool jump;
    public bool crouch;
    public bool slider;

    public void Awake() {
        instance = this;
        playerInputControl = new PlayerInputControl();
    }

    private void OnEnable() {
        playerInputControl.Enable();
    }

    void Update() {
        inputDirecion = playerInputControl.Gameplay.Move.ReadValue<Vector2>();
        jump = playerInputControl.Gameplay.Jump.IsPressed();
        crouch = playerInputControl.Gameplay.Crouch.IsPressed();
        slider = playerInputControl.Gameplay.Slider.IsPressed();
    }

    private void OnDisable() {
        playerInputControl.Disable();
    }
}
