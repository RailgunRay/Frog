using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float nutritionalValue;
    [SerializeField]
    protected float cost;
    private Player player;

    private void Start()
    {
        player = SpawnManager.player;
        Tongue.OnEnemyCatch += OnSelfCatched;
    }

    private void RestorePlayerHunger(float value)
    {
        player.hunger = Mathf.Clamp(player.hunger + value, 0, 100);
    }

    protected virtual void OnSelfCatched(Enemy enemy)
    {
        if (enemy == this)
        {
            RestorePlayerHunger(enemy.nutritionalValue);
            GameManager.ModifyScore(enemy.cost);
            Tongue.OnEnemyCatch -= OnSelfCatched;
            Destroy(enemy.gameObject);
        }
    }
}