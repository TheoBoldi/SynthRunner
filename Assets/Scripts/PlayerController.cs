using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Transform movePoint;
    [HideInInspector] public int actualPos;
    [HideInInspector] public List<Vector3> playerGrid;

    private Vector3 posLeft = new Vector3(-5, 0, 0);
    private Vector3 posCenter = new Vector3(0, 0, 0);
    private Vector3 posRight = new Vector3(5, 0, 0);
    private Rigidbody m_rb;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        m_rb = GetComponent<Rigidbody>();
        movePoint.parent = null;
        playerGrid.Add(posLeft);
        playerGrid.Add(posCenter);
        playerGrid.Add(posRight);
        actualPos = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        if (SwipeInput.swipedRight && actualPos < 2)
        {
            movePoint.position = playerGrid[actualPos + 1];
            actualPos += 1;
        }

        if (SwipeInput.swipedLeft && actualPos > 0)
        {
            movePoint.position = playerGrid[actualPos - 1];
            actualPos -= 1;
        }

        if (SwipeInput.swipedUp)
        {
            //Jump

        }

        if (SwipeInput.swipedDown)
        {
            //Dodge

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            gameManager.life -= 1;
        }

        if (other.CompareTag("Note"))
        {
            gameManager.score += 10;
        }
    }
}
