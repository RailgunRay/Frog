using UnityEngine;

public static class VectorExtensionMethods
{
    public static Vector3 Flat(this Vector3 original)
    {
        return new Vector3(original.x, original.y, 0f);
    }

    ///Returns angle in degrees///
    public static float GetAngleByDirection(this Vector3 origin, Camera viewCamera)
    {
        Vector2 direction = (viewCamera.ScreenToWorldPoint(Input.mousePosition) - 
                            origin).normalized;

        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public static Vector3 GetRandomPointOutsideOfTheViewport(Camera viewCamera)
    {
         int side = Random.Range(0, 4);

        float randomX, randomY;

        switch (side)
        {
            case 0:
                randomY = viewCamera.pixelHeight + 1f;
                randomX = Random.Range(0f, viewCamera.pixelWidth);
                var randomSpot = viewCamera.ScreenToWorldPoint(new Vector2(randomX, randomY));
                return randomSpot;
            case 1:
                randomY = Random.Range(0f, viewCamera.pixelHeight);
                randomX = viewCamera.pixelWidth + 1f;
                randomSpot = viewCamera.ScreenToWorldPoint(new Vector2(randomX, randomY));
                return randomSpot;
            case 2:
                randomY = -1f;
                randomX = Random.Range(0f, viewCamera.pixelWidth);
                randomSpot = viewCamera.ScreenToWorldPoint(new Vector2(randomX, randomY));
                return randomSpot;
            case 3:
                randomY = Random.Range(0f, viewCamera.pixelHeight);
                randomX = -1f;
                randomSpot = viewCamera.ScreenToWorldPoint(new Vector2(randomX, randomY));
                return randomSpot;
        }

        return Vector3.zero;
    }
}
