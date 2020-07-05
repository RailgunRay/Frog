using UnityEngine;

public static class Physics2DExtensionMethods
{
    public static RaycastHit2D RaycastOnClickedPosition(this RaycastHit2D hitInfo, Camera viewCamera)
    {
        Vector2 mousePos = viewCamera.ScreenToWorldPoint(Input.mousePosition);
        return Physics2D.Raycast(mousePos, Vector2.zero, 0f);
    }

    public static T GetComponentFromMousePositionClicked<T>(this RaycastHit2D hitInfo, Camera viewCamera)
    {
        hitInfo = RaycastOnClickedPosition(hitInfo, viewCamera);
        Transform clickedTransform = hitInfo.transform;

        if(clickedTransform)
        {
            return clickedTransform.GetComponent<T>();
        }

        return default(T);
    }
}
