using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player instance;
#pragma warning disable 0649
    [SerializeField]
    private float startingMaxJumpLength = 5.5f;

    [SerializeField]
    private float jumpCoefficient = .05f;
    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float hungerModifyer;

    [SerializeField]
    private float jumpSpeed = 10f;
#pragma warning restore 0649
    public static bool isJumping;
    public float hunger;
    public float jumpLength;
    private Tongue tongue;
    private Camera viewCamera;

    private void Awake()
    {
        viewCamera = Camera.main;
        tongue = GetComponentInChildren<Tongue>();
        hunger = 100f;
        jumpLength = startingMaxJumpLength;
    }

    private void Start()
    {
        Tongue.OnEnemyCatch += Grow;
    }

    private void Update()
    {
        hunger -= Time.deltaTime * hungerModifyer;
    }

    internal void FaceClickedPosition(Action Callback = null)
    {
        float desiredAngle = transform.position.GetAngleByDirection(viewCamera);
        StartCoroutine(RotatePlayerRoutine(desiredAngle, Callback));
    }

    internal void ThrowTongue()
    {
        tongue.ThrowTongue();
    }

    internal void JumpToAnotherWaterLily(Transform anotherLily)
    {
        StartCoroutine(RepositionPlayerRoutine(anotherLily.position));
    }

    private void Grow(Enemy enemy = null)
    {
        if (transform.localScale.x < 1)
        {
            transform.localScale *= 1.01f;
            IncreaseHungerModifyer();
            IncreaseJumpLength();
        }
    }

    private void IncreaseJumpLength()
    {
        jumpLength += jumpCoefficient;
    }

    private void IncreaseHungerModifyer()
    {
        hungerModifyer += .1f;
    }

    private IEnumerator RotatePlayerRoutine(float Zangle, Action Callback = null)
    {
        while (true)
        {
            Vector3 desiredRotation = new Vector3(0, 0, Zangle);
            transform.eulerAngles = new Vector3(0, 0, Mathf.MoveTowardsAngle(transform.eulerAngles.z,
                Zangle, rotationSpeed * Time.deltaTime));

            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, Zangle)) < 1f)
            {
                break;
            }

            yield return null;
        }

        Callback?.Invoke();
    }

    private IEnumerator RepositionPlayerRoutine(Vector2 newPosition)
    {
        while (Vector2.Distance(transform.position, newPosition) > .2f)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                newPosition, jumpSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = new Vector3(newPosition.x, newPosition.y, -5);
        PlayerInput.jumping = false;
    }
}
