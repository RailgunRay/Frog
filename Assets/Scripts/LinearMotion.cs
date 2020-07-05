using UnityEngine;
using System.Collections;

public class LinearMotion : MonoBehaviour
{
    private IEnumerator linearMotionRoutine;

    #pragma warning disable 0649
    [SerializeField]
    private Transform transformToMove;

    [SerializeField]
    private Vector2 motionSpeedMinMax = new Vector2(5f, 7f);

    [SerializeField]
    private Vector2 delayBeforeFlightToTheNextPointMinMax = new Vector2(.5f, 1.2f);
    #pragma warning restore 0649

    private float moveSpeed;

    private void Start()
    {
        moveSpeed = Random.Range(motionSpeedMinMax.x, motionSpeedMinMax.y);
        linearMotionRoutine = LinearMotionRoutine();
        StartCoroutine(linearMotionRoutine);
    }

    public void StopMotion()
    {
        StopCoroutine(linearMotionRoutine);
    }

    private IEnumerator LinearMotionRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(.1f, .9f);
            float randomY = Random.Range(.1f, .9f);
            Vector3 newPos = Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, 0));
            Vector3 zCorrectedPosition = new Vector3(newPos.x, newPos.y, transform.position.z);

            while (Vector2.Distance(transform.position, newPos) > .1f)
            {
                transformToMove.position = Vector3.MoveTowards
                (transformToMove.position, zCorrectedPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            float delay = Random.Range(delayBeforeFlightToTheNextPointMinMax.x, 
                                       delayBeforeFlightToTheNextPointMinMax.y);
            yield return new WaitForSeconds(delay);
        }
    }
}
