using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private GameObject teleportationVFX;

    private PlayerController playerController_script;


    // Start is called before the first frame update
    void Start()
    {

        playerController_script = GetComponent<PlayerController>();
        playerController_script.TeleportEvent += Teleportation_PS;
    }

    private void Teleportation_PS()
    {
        Instantiate(teleportationVFX, transform.position, Quaternion.identity);
    }


}
