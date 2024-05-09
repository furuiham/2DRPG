using UnityEngine;

public class InputController : MonoBehaviour {
    public PlayerInputControl playerInputControl;

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
