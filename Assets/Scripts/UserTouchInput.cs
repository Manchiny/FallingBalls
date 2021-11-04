using UnityEngine;

public class UserTouchInput : MonoBehaviour
{
    private void Awake()
    {
        if (Input.touchSupported == true)
        {
            Input.simulateMouseWithTouches = false;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                if (hit.collider?.GetComponent<Ball>())
                {
                    hit.transform.GetComponent<Ball>().AddPlayerPoints();
                }
            }
        }
    }
}
