using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

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
                CameraShaker.Instance.ShakeOnce(8f, 4f, .1f, 1f);
                SoundEffectManager.instance.HitObstacle();
                if(!PlayerController.isHit)
                    PlayerController.isHit = true;
                else if(PlayerController.switchBack)
                    GameManager.Instance.life = 0;
                Destroy(this.gameObject);
            }

            if (this.tag == "Purple" || this.tag == "Blue" || this.tag == "Green")
            {
                if (PlayerController.inCharge)
                {
                    SoundEffectManager.instance.GoodNote();
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
