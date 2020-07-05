using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    private Player player;
    private Camera viewCamera;
    private IEnumerator ObserveClicks;
    private WaterLily clickedLily;
    private int timesClicked = 0;
    private Vector2 screenPosClicked;
    public static bool jumping;

    [SerializeField]
    private float awaitTime = .2f;

    private void Awake()
    {
        viewCamera = Camera.main;
        Physics2D.queriesHitTriggers = true;
        ObserveClicks = ObserveTimesClicked();
    }

    private void Start()
    {
        player = SpawnManager.player;
        StartCoroutine(ObserveClicks);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            screenPosClicked = viewCamera.ScreenToWorldPoint(Input.mousePosition);
            if (!jumping)
            {
                HandleMouseClick();
            }
        }
    }

    private void HandleMouseClick()
    {
        RaycastHit2D hit = new RaycastHit2D();
        WaterLily lily = hit.GetComponentFromMousePositionClicked<WaterLily>(viewCamera);

        if (!lily || !lily.isReachable)
        {
            player.FaceClickedPosition(player.ThrowTongue);
        }
        else
        {
            if (lily == clickedLily)
            {
                timesClicked++;
            }
            else
            {
                clickedLily = lily;
                timesClicked = 0;
                timesClicked++;
            }
        }
    }

    private IEnumerator ObserveTimesClicked()
    {
        float timeElapsed = 0;

        while (true)
        {
            if (timesClicked == 1)
            {
                timeElapsed += Time.deltaTime;
                if (timeElapsed >= awaitTime)
                {
                    timeElapsed = 0;
                    timesClicked = 0;
                    clickedLily = null;
                    player.FaceClickedPosition(player.ThrowTongue);
                }
            }
            else if (timesClicked >= 2 && !jumping)
            {
                jumping = true;
                player.FaceClickedPosition(delegate
                                        {
                                            player.JumpToAnotherWaterLily(clickedLily.transform);
                                        });
                timeElapsed = 0;
                timesClicked = 0;
            }
            yield return null;
        }
    }
}
