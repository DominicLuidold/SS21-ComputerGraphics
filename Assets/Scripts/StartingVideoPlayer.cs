using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Events;

public class StartingVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    public UnityEvent startingVideoFinished = new UnityEvent();

    void Start()
    {
        _videoPlayer.loopPointReached += EndReached;
    }

    void EndReached(VideoPlayer vp)
    {
        startingVideoFinished.Invoke();
    }
}
