using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    public void HandleMovement()
    {
        Vector3 horizontal = Input.GetAxis("Horizontal") * transform.right;
        Vector3 forward = Input.GetAxis("Forward") * transform.forward;

        Vector3 moveDirection = (horizontal + forward).normalized;
        moveDirection.y = 0;
        Vector3 velocity = moveDirection * moveSpeed * Time.deltaTime;

        controller.Move(velocity);
    }

}
