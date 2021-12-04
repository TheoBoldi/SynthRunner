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
                SoundEffectManager.instance.HitObstacle();
                GameManager.Instance.life -= 1;
                Destroy(this.gameObject);
            }

            if (this.tag == "Purple" || this.tag == "Blue" || this.tag == "Green")
            {
                if (PlayerController.inCharge)
                {
                    SoundEffectManager.instance.GoodNote();
                    GameManager.Instance.score += 10;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
