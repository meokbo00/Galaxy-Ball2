using UnityEngine;
using UnityEngine.SceneManagement;

public class P1Deadzone : MonoBehaviour
{
    public bool isExpand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1ball")
        {
            MultiBallController ball = collision.GetComponent<MultiBallController>();
            if (!ball.isExpanding)
                SceneManager.LoadScene("P2 Win");
        }


        if (collision.gameObject.tag == "P1Item")
        {
            switch (collision.gameObject.name)
            {
                case "BlackHoleF(Clone)":
                    BlackHole_Skill skill = collision.GetComponent<BlackHole_Skill>();
                    this.isExpand = skill.isExpanding;
                    break;
                case "InvincibleF(Clone)":
                    Invincible_Skill skill5 = collision.GetComponent<Invincible_Skill>();
                    this.isExpand = skill5.isExpanding;
                    break;
                case "BigF(Clone)":
                    MultiBallController bigitem = collision.GetComponent<MultiBallController>();
                    this.isExpand = bigitem.isExpanding;
                    break;
                case "SmallF(Clone)":
                    MultiBallController smallitem = collision.GetComponent<MultiBallController>();
                    this.isExpand = smallitem.isExpanding;
                    break;
                case "TwiceF(Clone)":
                    MultiBallController twiceitem = collision.GetComponent<MultiBallController>();
                    this.isExpand = twiceitem.isExpanding;
                    break;
            }
            if (isExpand == false)
            {
                SceneManager.LoadScene("P2 Win");
            }
        }
    }
}