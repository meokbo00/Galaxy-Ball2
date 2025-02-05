using UnityEngine;

public class StageState : MonoBehaviour
{
    public GameObject StageStart;
    public GameObject StartButton;
    public GameObject Clearhere;
    public int stagenum;
    public static int chooseStage;
    private bool isclear;
    private StageGameManager gameManager;
    private SpriteRenderer spriteRenderer;

    public bool isturn = false; // 초기 움직임 상태
    public float rotationSpeed = 1f;
    public float radius = 5f;
    public float initialAngle;

    private Vector3 initialPosition;
    private float angle = 0f;
    private bool wasInitiallyMoving; // 처음에 움직이고 있었는지 여부 저장

    void Start()
    {
        string namePart = gameObject.name.Substring(5, 2); // 6번째와 7번째 문자를 추출
        this.stagenum = int.Parse(namePart);
        gameManager = StageGameManager.instance;
        float stageClearID = gameManager.StageClearID;

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position; // 초기 위치 저장
        initialAngle = Random.Range(0f, 360f); // 초기 각도를 랜덤으로 설정
        angle = initialAngle; // 초기 각도로 설정

        wasInitiallyMoving = isturn; // 초기 움직임 상태 저장

        if (stageClearID < this.stagenum)
        {
            isclear = false;
            spriteRenderer.color = new Color32(100, 100, 100, 255);
        }
        else if (stageClearID == this.stagenum)
        {
            isclear = true;
            spriteRenderer.color = new Color32(100, 100, 100, 255);

            if (Clearhere != null)
            {
                Instantiate(Clearhere, transform.position, Quaternion.identity, transform);
            }
        }
        else
        {
            isclear = true;
            spriteRenderer.color = new Color32(255, 255, 255, 255);
        }
    }

    void Update()
    {
        if (isturn)
        {
            // 원을 그리며 이동
            angle += rotationSpeed * Time.deltaTime;
            if (angle > 360f) angle -= 360f; // 각도가 360도를 넘지 않도록 조정

            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            transform.position = initialPosition + new Vector3(x, y, 0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
        {
            Debug.Log("스테이지 플레이창을 띄웁니다");
            StageStart.gameObject.SetActive(true);
            isturn = false; // 움직임 멈춤

            if (!isclear)
            {
                StartButton.gameObject.SetActive(false);
            }
            else if (isclear)
            {
                StartButton.gameObject.SetActive(true);
            }
            chooseStage = stagenum;
            Debug.Log("chooseStage : " + chooseStage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "StageBall")
        {
            StageStart.gameObject.SetActive(false);

            // 처음부터 움직이던 상태였던 경우에만 다시 움직임을 시작
            if (wasInitiallyMoving)
            {
                isturn = true;
            }
        }
    }
}
