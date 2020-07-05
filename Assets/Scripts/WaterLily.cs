using UnityEngine;

public class WaterLily : MonoBehaviour
{
    private Player player;
    private Behaviour halo;
    private bool isClicked;
    private float lastTimeClicked = -1f;
    private float maxJumpLength;
    public bool isReachable;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        maxJumpLength = player.jumpLength;
        halo = (Behaviour)GetComponent("Halo");
        Tongue.OnEnemyCatch += UpdateJumpLength;
    }

    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance > .2f && distance < maxJumpLength)
        {
            halo.enabled = true;
            isReachable = true;
        }
        else
        {
            halo.enabled = false;
            isReachable = false;
            isClicked = false;
        }
    }

    private void UpdateJumpLength(Enemy enemy = null)
    {
        maxJumpLength = player.jumpLength;
    }
}
