using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    public AudioSource hitObstacle;
    public AudioSource goodNote;
    public AudioSource launchGame;
    public AudioSource playerMove;
    public AudioSource title;

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

    public void LaunchGame()
    {
        launchGame.Play();
    }

    public void PlayerMove()
    {
        playerMove.pitch = Random.Range(0.9f, 1.1f);
        playerMove.Play();
    }
}
