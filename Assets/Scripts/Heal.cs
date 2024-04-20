using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Check if the player has less than 3 hearts before adding health
            if (Heartsystem.health < 3)
            {
                Heartsystem.health += 1;
                Destroy(gameObject);
            }
        }
    }
}
