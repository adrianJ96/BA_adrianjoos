using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoSwitcher : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to VideoPlayer
    public VideoClip[] videoClips; // List of video clips 

    private int currentVideoIndex = 0;

    void Start()
    {
        if (videoClips.Length > 0)
        {
            videoPlayer.clip = videoClips[currentVideoIndex];
            videoPlayer.Play();
        }
    }

    // Switch video in two directions
    public void NextVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex = (currentVideoIndex + 1) % videoClips.Length;
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
    }

    public void PreviousVideo()
    {
        if (videoClips.Length == 0) return;

        currentVideoIndex = (currentVideoIndex - 1 + videoClips.Length) % videoClips.Length;
        videoPlayer.clip = videoClips[currentVideoIndex];
        videoPlayer.Play();
    }
}
