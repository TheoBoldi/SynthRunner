using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour {

	public List<touchLocation> touches = new List<touchLocation>();

	public const float MAX_SWIPE_TIME = 0.5f; 
	public const float MIN_SWIPE_DISTANCE = 0.05f;

	public static bool swipedRight = false;
	public static bool swipedLeft = false;
	public static bool swipedUp = false;
	public static bool swipedDown = false;
	
	
	public bool debugWithArrowKeys = true;

	Vector2 startPos;
	float startTime;

	public void Update()
	{
		swipedRight = false;
		swipedLeft = false;
		swipedUp = false;
		swipedDown = false;

		int i = 0;
		while (i < Input.touchCount)
        {
			if (Input.touches.Length > 0)
			{
				Touch t = Input.GetTouch(i);
				if (t.phase == TouchPhase.Began)
				{
					startPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
					startTime = Time.time;
				}
				if (t.phase == TouchPhase.Ended)
				{
					touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);

					if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
						return;

					Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

					Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

					if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
						return;

					if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
					{ // Horizontal swipe
						if (swipe.x > 0)
						{
							swipedRight = true;
						}
						else
						{
							swipedLeft = true;
						}
					}
					else
					{ // Vertical swipe
						if (swipe.y > 0)
						{
							swipedUp = true;
						}
						else
						{
							swipedDown = true;
						}
					}
				}

				else if (t.phase == TouchPhase.Moved)
				{
					touchLocation thisTouch = touches.Find(touchLocation => touchLocation.touchId == t.fingerId);

					if (thisTouch != null && thisTouch.item != null)
						thisTouch.item.transform.position = GetTouchPosition(t.position);
				}
				i++;
			}
		}

		if (debugWithArrowKeys) {
			swipedDown = swipedDown || Input.GetKeyDown (KeyCode.DownArrow);
			swipedUp = swipedUp|| Input.GetKeyDown (KeyCode.UpArrow);
			swipedRight = swipedRight || Input.GetKeyDown (KeyCode.RightArrow);
			swipedLeft = swipedLeft || Input.GetKeyDown (KeyCode.LeftArrow);
		}
	}

	Vector2 GetTouchPosition(Vector2 touchPosition)
	{
		return Camera.main.ScreenToWorldPoint(touchPosition);
	}
}