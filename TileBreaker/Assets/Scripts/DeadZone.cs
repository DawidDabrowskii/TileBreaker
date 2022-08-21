using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
   private void OnCollisionEnter2D(Collision2D collision) // if ball hits deadzone
    {
        if (collision.gameObject.name == "Ball")
            FindObjectOfType<GameManager>().Miss();
    }
}
