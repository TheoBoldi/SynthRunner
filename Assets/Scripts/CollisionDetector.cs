using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void Update()
    {
        if(transform.position.z < -10f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(this.tag == "Wall")
            {
                GameManager.Instance.life -= 1;
                Destroy(this.gameObject);
            }

            if (this.tag == "Note")
            {
                GameManager.Instance.score += 10;
                Destroy(this.gameObject);
            }
        }
    }
}
