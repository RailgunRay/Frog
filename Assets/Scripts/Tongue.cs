using System;
using System.Collections;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    public static event Action<Enemy> OnEnemyCatch = delegate { };
    public static bool throwingTongue;
    public Vector2 tongueLengthMinMax;

    [SerializeField]
    private float tongueSpeed = 3f;

    [SerializeField]
    private LayerMask whatIsEnemy = default(LayerMask);

    private bool hitEnemy = false;
    private float raycastMaxDistance;
    private Transform tongueGraphics;
    private LineRenderer tongueLine;
    private EnemyCatcher enemyCatcher;

    private void Awake()
    {
        OnEnemyCatch = delegate { };
        enemyCatcher = GetComponentInChildren<EnemyCatcher>();
        raycastMaxDistance = GetComponentInChildren<Collider2D>().bounds.extents.y + .07f;
    }

    private void Start()
    {
        tongueGraphics = transform.GetChild(0);
        tongueLine = GetComponent<LineRenderer>();
        tongueLine.SetPosition(0, Vector3.zero);
        SetTongueWidth(.3f);
    }

    private void SetTongueWidth(float newScale)
    {
        tongueGraphics.localScale = new Vector3(newScale, newScale, newScale);
        tongueLine.widthMultiplier = tongueGraphics.localScale.x;
    }

    public void ThrowTongue()
    {
        StartCoroutine(ThrowTongueRoutine());
    }

    private void RaycastFromTongue()
    {
        float alpha = 90 - Mathf.Atan2(-transform.right.y, transform.right.x) * Mathf.Rad2Deg;
        for (int i = 0; i < 5; i++)
        {
            Vector3 dirVector = new Vector2(Mathf.Cos(alpha * Mathf.Deg2Rad),
                                            Mathf.Sin(alpha * Mathf.Deg2Rad)).normalized;

            RaycastHit2D hit = Physics2D.Raycast(tongueGraphics.position, dirVector,
            tongueSpeed * Time.fixedDeltaTime + .4f, whatIsEnemy);
            // Debug.DrawRay(tongueGraphics.position, dirVector * (tongueSpeed * Time.deltaTime), Color.green);

            if (hit.transform != null)
            {
                if (hit.transform.tag == "Enemy")
                    ProcessHit(hit.transform.GetComponentInParent<Enemy>());
            }
            alpha -= 45;
        }
    }

    private void ProcessHit(Enemy enemy)
    {
        hitEnemy = true;
        OnEnemyCatch(enemy);
    }

    private IEnumerator ThrowTongueRoutine()
    {
        throwingTongue = true;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        float percent = 0;
        while (percent <= 1)
        {
            if (hitEnemy) percent = 1;
            if (percent < .5f) RaycastFromTongue();
            percent += Time.deltaTime * tongueSpeed;
            float length = Mathf.Lerp(tongueLengthMinMax.x,
                            tongueLengthMinMax.y, Difficulty.GetDifficultyPercent());
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            tongueGraphics.localPosition = Vector3.Lerp(Vector3.zero, Vector3.right * length,
            interpolation);
            tongueLine.SetPosition(1, tongueGraphics.localPosition);
            yield return null;
        }
        hitEnemy = false;
        throwingTongue = false;
    }
}
