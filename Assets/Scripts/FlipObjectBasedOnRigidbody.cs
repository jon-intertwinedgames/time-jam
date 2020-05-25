using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipObjectBasedOnRigidbody : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb = null;

    // Update is called once per frame
    void Update()
    {
        if (rb)
        {
            if (rb.velocity.x > 0)
            {
                if (this.transform.localScale.x < 0)
                    this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
            }

            else if (rb.velocity.x < 0)
            {
                if (this.transform.localScale.x > 0)
                    this.transform.localScale = new Vector3(this.transform.localScale.x * -1, this.transform.localScale.y, this.transform.localScale.z);
            }
        }

        else
        {
            //do nothing
        }
    }
}
