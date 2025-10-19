using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float runDrainRate = 15f;
    public float regenRate = 10f;

    [Header("Speed Settings")]
    public float walkSpeed = 4f;
    public float runSpeed = 8f;
    public float crouchSpeed = 2f;

    [Header("References")]
    public Slider staminaSlider;
    private CharacterController controller;

    private bool isRunning = false;
    private bool isCrouching = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        if (staminaSlider != null)
            staminaSlider.maxValue = maxStamina;
    }

    void Update()
    {
        HandleCrouch();
        HandleMovementAndStamina();
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }

    void HandleMovementAndStamina()
    {
        bool moving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
        isRunning = Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && moving && !isCrouching;

        if (isRunning)
        {
            currentStamina -= runDrainRate * Time.deltaTime;
        }
        else
        {
            currentStamina += regenRate * Time.deltaTime;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        float speed = walkSpeed;
        if (isRunning) speed = runSpeed;
        else if (isCrouching) speed = crouchSpeed;

        MovePlayer(speed);

        if (staminaSlider != null)
            staminaSlider.value = currentStamina;
    }

    void MovePlayer(float speed)
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);
        controller.Move(move * speed * Time.deltaTime);
    }
}
