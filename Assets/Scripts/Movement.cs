using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    IMovementInput input;
    Rigidbody2D rb;

    [SerializeField]
    float movementSpeed = 1.0f;

    Animator anim;

    // Runs before first frame update
    void Start()
    {
        input = GetComponent<IMovementInput>();
        rb = GetComponent<Rigidbody2D>();

        TryGetComponent(out anim); // Attempt to assign the animator
    }

    // Update is called once per frame
    void Update()
    {
        bool walking = false;

        // The absolute value disregards signs, meaning that negative values can also
        // appear greater than 0.1
        if (Mathf.Abs(input.GetHorizontal()) > 0.1f)
        {
            rb.velocity = transform.right * movementSpeed * (input.GetHorizontal() > 0 ? 1 : -1);
            walking = true;
        }
        else if(Mathf.Abs(input.GetVertical()) > 0.1f)
        {
            rb.velocity = transform.up * movementSpeed * (input.GetVertical() > 0 ? 1 : -1);
            walking = true;
        }
        // If the player isn't moving then we zero out their velocity
        // to stop them from moving
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (anim)
        {
            anim.SetBool("Walking", walking);
        }
    }
}
