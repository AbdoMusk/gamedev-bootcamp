using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool generatePrimitiveObstacles = true;
    [SerializeField] private GameObject visualPrefab;
    [SerializeField] private float destroyX = -15f;
    [SerializeField] private float obstacleTopY = 11f;
    [SerializeField] private float obstacleBottomY = -0.5f;
    [SerializeField] private float obstacleWidth = 1.5f;
    [SerializeField] private float obstacleDepth = 1.5f;
    [SerializeField] private float scoreZoneDepth = 0.5f;
    [SerializeField] private float visualTopRotationX = 90f;
    [SerializeField] private float visualBottomRotationX = -90f;

    private const float LaneCenterFactor = 0.5f;
    private bool hasScored = false;
    private bool isStopped = false;

    private void Update()
    {
        if (isStopped) return;
        
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        if (transform.position.x < destroyX)
            Destroy(gameObject);
    }

    public void Stop()
    {
        isStopped = true;
    }

    public void Initialize(int lane, float gapY, float gapSize, int laneCount, float laneSpacing, float spawnX)
    {
        float laneZ = GetLaneZ(lane, laneCount, laneSpacing);
        transform.position = new Vector3(spawnX, 0f, laneZ);
        CreateObstacles(gapY, gapSize);
    }

    public void SetGeneratePrimitives(bool generate)
    {
        generatePrimitiveObstacles = generate;
    }

    public void SetVisualPrefab(GameObject prefab)
    {
        visualPrefab = prefab;
    }

    private void CreateObstacles(float gapY, float gapSize)
    {
        float halfGap = gapSize / 2f;

        if (generatePrimitiveObstacles || visualPrefab == null)
        {
            if (gapY + halfGap < obstacleTopY)
            {
                GameObject top = GameObject.CreatePrimitive(PrimitiveType.Cube);
                top.transform.parent = transform;
                top.transform.localPosition = new Vector3(0f, gapY + halfGap + (obstacleTopY - gapY - halfGap) / 2f, 0f);
                top.transform.localScale = new Vector3(obstacleWidth, obstacleTopY - gapY - halfGap, obstacleDepth);
                top.tag = "Obstacle";
                DestroyImmediate(top.GetComponent<Collider>());
                BoxCollider topBox = top.AddComponent<BoxCollider>();
                topBox.isTrigger = true;
            }

            if (gapY - halfGap > obstacleBottomY)
            {
                GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                bottom.transform.parent = transform;
                bottom.transform.localPosition = new Vector3(0f, (gapY - halfGap - obstacleBottomY) / 2f, 0f);
                bottom.transform.localScale = new Vector3(obstacleWidth, gapY - halfGap - obstacleBottomY, obstacleDepth);
                bottom.tag = "Obstacle";
                DestroyImmediate(bottom.GetComponent<Collider>());
                BoxCollider bottomBox = bottom.AddComponent<BoxCollider>();
                bottomBox.isTrigger = true;
            }
        }
        else
        {
            if (gapY + halfGap < obstacleTopY)
            {
                GameObject top = Instantiate(visualPrefab, transform);
                top.transform.localEulerAngles = new Vector3(visualTopRotationX, 0f, 0f);
                top.tag = "Obstacle";
                EnsureTriggerCollider(top);
                AlignBoundsBottom(top, gapY + halfGap);
            }

            if (gapY - halfGap > obstacleBottomY)
            {
                GameObject bottom = Instantiate(visualPrefab, transform);
                bottom.transform.localEulerAngles = new Vector3(visualBottomRotationX, 0f, 0f);
                bottom.tag = "Obstacle";
                EnsureTriggerCollider(bottom);
                AlignBoundsTop(bottom, gapY - halfGap);
            }
        }

        GameObject score = new GameObject("ScoreZone");
        score.transform.parent = transform;
        score.transform.localPosition = new Vector3(0f, gapY, 0f);
        score.tag = "ScoreZone";
        BoxCollider scoreBox = score.AddComponent<BoxCollider>();
        scoreBox.isTrigger = true;
        scoreBox.size = new Vector3(obstacleWidth, gapSize, scoreZoneDepth);
    }

    private void EnsureTriggerCollider(GameObject obj)
    {
        Collider existing = obj.GetComponent<Collider>();
        if (existing == null)
        {
            BoxCollider box = obj.AddComponent<BoxCollider>();
            box.isTrigger = true;
        }
        else
        {
            existing.isTrigger = true;
        }
    }

    private void AlignBoundsBottom(GameObject obj, float targetBottomY)
    {
        Bounds bounds = GetObjectBounds(obj);
        float deltaY = targetBottomY - bounds.min.y;
        obj.transform.position += new Vector3(0f, deltaY, 0f);
    }

    private void AlignBoundsTop(GameObject obj, float targetTopY)
    {
        Bounds bounds = GetObjectBounds(obj);
        float deltaY = targetTopY - bounds.max.y;
        obj.transform.position += new Vector3(0f, deltaY, 0f);
    }

    private Bounds GetObjectBounds(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length > 0)
        {
            Bounds bounds = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }
            return bounds;
        }

        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        if (colliders.Length > 0)
        {
            Bounds bounds = colliders[0].bounds;
            for (int i = 1; i < colliders.Length; i++)
            {
                bounds.Encapsulate(colliders[i].bounds);
            }
            return bounds;
        }

        return new Bounds(obj.transform.position, Vector3.one);
    }

    private float GetLaneZ(int lane, int laneCount, float laneSpacing)
    {
        if (laneCount <= 1) return 0f;
        float centerIndex = (laneCount - 1) * LaneCenterFactor;
        return (lane - centerIndex) * laneSpacing;
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
