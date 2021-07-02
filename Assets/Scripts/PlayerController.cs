using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 20f;

    [SerializeField] float xRange = 13f; // for claming the position
    [SerializeField] float yRange = 6f; // for claming the position

    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;

    [SerializeField] float positionYawFactor = -2f;

    [SerializeField] float controlRollFactor = -10f;

    float yThrow;
    float xThrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable() {
        movement.Enable();
    }

    private void OnDisable() {
        movement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow*controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;



        float yaw = transform.localPosition.x * positionYawFactor;


        float roll = xThrow * controlRollFactor;


        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        // Debug.Log(xThrow);
        yThrow = movement.ReadValue<Vector2>().y;
        // Debug.Log(yThrow);

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);

        // OldSystem
        // float horizontalThrow = Input.GetAxis("Horizontal");
        // Debug.Log(horizontalThrow);
        // float verticalThrow = Input.GetAxis("Vertical");
        // Debug.Log(verticalThrow);
    }
}
