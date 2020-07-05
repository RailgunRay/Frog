using UnityEngine;
using System.Collections;

public class CircularMotion : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField]
    private float rotationSpeed;

    [SerializeField]
    private Vector2 radiusMinMax;

    [SerializeField]
    private Transform targetGraphicsToMove;
    #pragma warning restore 0649
    
    private float angle;
    private float radius;

    private void Awake()
    {
        angle = Random.Range(0, 360f);
        radius = Random.Range(radiusMinMax.x, radiusMinMax.y);
    }

    private void Update()
    {
        angle += rotationSpeed * Time.deltaTime;

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        targetGraphicsToMove.localPosition = new Vector3(x, y, 0);
    }
}
