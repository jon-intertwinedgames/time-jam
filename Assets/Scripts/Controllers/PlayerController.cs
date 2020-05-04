using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Protagonist protagonist_script;
    private LandMovement movement_script;
    private Aimer aimer_script;
    // Start is called before the first frame update
    void Start()
    {
        protagonist_script = GetComponent<Protagonist>();
        movement_script = GetComponent<LandMovement>();
        aimer_script = GetComponentInChildren<Aimer>();
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        ArrowInput();
    }

    void MovementInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Jump");

        movement_script.Move(h, v);
    }

    void ArrowInput()
    {
        if(Input.GetMouseButtonUp(0))
        {
            Vector2 mouseInWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 projectileDirection = aimer_script.GetDirection(transform.position, mouseInWorldPoint);
            aimer_script.CreateProjectile(transform.position, projectileDirection, "arrow");
        }
    }
}
