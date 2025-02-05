using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            switch (gameObject.name)
            {
                case "B":
                    rb.position += new Vector2(0, 170);
                    break;
                case "T":
                    rb.position += new Vector2(0, -170);
                    break;
                case "L":
                    rb.position += new Vector2(160, 0);
                    break;
                case "R":
                    rb.position += new Vector2(-160, 0);
                    break;
            }
        }
    }
}
