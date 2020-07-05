using UnityEngine;

public class EnemyCatcher : MonoBehaviour
{
    public void Catch(Enemy enemy)
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Tongue.throwingTongue)
        {
            // IDraggable draggable = other.transform.parent.GetComponent<IDraggable>();
            // if (draggable != null)
            // {
            //     draggable.Drag(transform);
            // }
        }
    }
}