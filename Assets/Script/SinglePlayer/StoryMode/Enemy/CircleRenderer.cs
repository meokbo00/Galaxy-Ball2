using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer : MonoBehaviour
{
    public float radius = 1f; 

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.useWorldSpace = false;
        lineRenderer.startColor = new Color(1f, 0f, 0f);
        lineRenderer.endColor = new Color(1f, 0f, 0f); 
        lineRenderer.sortingOrder = 3; 
        RenderCircle();
    }

    void RenderCircle()
    {
        int segments = 360;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;

        float angle = 0f;
        for (int i = 0; i <= segments; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0));

            angle += (360f / segments);
        }
    }
}