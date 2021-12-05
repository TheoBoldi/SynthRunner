using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    public AudioSource hitObstacle;
    public AudioSource goodNote;

    private void Awake()
    {
        instance = this;
    }

    public void HitObstacle()
    {
        hitObstacle.Play();
    }

    public void GoodNote()
    {
        goodNote.Play();
    }
}
