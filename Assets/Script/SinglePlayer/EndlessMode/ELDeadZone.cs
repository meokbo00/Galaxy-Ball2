using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ELDeadZone : MonoBehaviour
{
    public bool isExpand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("P1ball"))
        {
            ExBallController ball = collision.GetComponent<ExBallController>();
            if (ball != null && !ball.isExpanding)
            {
                SceneManager.LoadScene("ELFail");
                return;
            }
        }


        if (collision.gameObject.tag == "Item")
        {
            switch (collision.gameObject.name)
            {
                case "SPEndlessF(Clone)":
                    this.isExpand = true;
                    break;
                case "SPBlackHoleF(Clone)":
                    BlackHole_Skill skill = collision.GetComponent<BlackHole_Skill>();
                    this.isExpand = skill.hasExpanded;
                    break;
                case "SPInvincibleF(Clone)":
                    Invincible_Skill skill5 = collision.GetComponent<Invincible_Skill>();
                    this.isExpand = skill5.hasExpanded;
                    break;
            }
            if (isExpand == false)
            {
                SceneManager.LoadScene("ELFail");
            }
        }
    }
}