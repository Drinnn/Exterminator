using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UI.Joystick joystick;

    [SerializeField] private float moveSpeed = 20f;
    
    private CharacterController _characterController;
    
    private Vector2 _moveInput;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        joystick.OnStickValueUpdated += Joystick_OnStickValueUpdated;
    }

    private void Joystick_OnStickValueUpdated(Vector2 inputValue)
    {
        _moveInput = inputValue;
    }

    private void Update()
    {
        _characterController.Move(new Vector3(_moveInput.x, 0, _moveInput.y) * (Time.deltaTime * moveSpeed));
    }
}
