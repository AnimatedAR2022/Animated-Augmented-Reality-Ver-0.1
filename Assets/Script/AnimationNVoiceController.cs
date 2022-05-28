using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationNVoiceController : MonoBehaviour
{
    public AudioSource audioPlayer;
    public List<AudioClip> HDDClips;
    public List<AudioClip> USBClips;
    public List<AudioClip> OPSClips;

    public Button forwardButton;
    public Button backwardButton;
    public Button playButton;
    public Button voiceControlButton;

    private List<AudioClip> currentClips;
    private bool playAudio = false;

    private void FixedUpdate()
    {
        if (!playAudio) return;

        if (!audioPlayer.isPlaying)
        {
            EndOfClipCheck();
        }
    }

    private void EndOfClipCheck()
    {
        int currentPlayIndex = currentClips.IndexOf(audioPlayer.clip);
        if (currentPlayIndex >= currentClips.Count - 1)
        {
            ResetPlayBack();
        }
        else
        {
            NextClip();
        }
    }

    public void SetupPlayBack(int clipNo)
    {
        switch (clipNo)
        {
            case 1:
                currentClips = HDDClips;
                break;

            case 2:
                currentClips = USBClips;
                break;

            case 3:
                currentClips = OPSClips;
                break;
        }

        audioPlayer.clip = currentClips[0];
        audioPlayer.mute = false;
        voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_unmute_icon");
        forwardButton.interactable = true;
        backwardButton.interactable = false;
        playButton.interactable = true;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        voiceControlButton.interactable = true;
    }

    public void DisablePlayBack()
    {
        currentClips = null;
        audioPlayer.clip = null;
        audioPlayer.mute = false;
        voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_unmute_icon");
        forwardButton.interactable = false;
        backwardButton.interactable = false;
        playButton.interactable = false;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        voiceControlButton.interactable = false;
    }

    public void ResetPlayBack()
    {
        playAudio = false;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        audioPlayer.Stop();
        audioPlayer.clip = currentClips[0];
        forwardButton.interactable = true;
        backwardButton.interactable = false;
    }

    public void NextClip()
    {
        if (audioPlayer.isPlaying) audioPlayer.Stop();
        int currentPlayIndex = currentClips.IndexOf(audioPlayer.clip);
        audioPlayer.clip = currentClips[++currentPlayIndex];
        audioPlayer.Play();
        playAudio = true;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/pause_icon");
        if (currentPlayIndex >= currentClips.Count - 1)
        {
            forwardButton.interactable = false;
        }
        backwardButton.interactable = true;
    }

    public void PreviousClip()
    {
        if (audioPlayer.isPlaying) audioPlayer.Stop();
        int currentPlayIndex = currentClips.IndexOf(audioPlayer.clip);
        audioPlayer.clip = currentClips[--currentPlayIndex];
        audioPlayer.Play();
        playAudio = true;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/pause_icon");
        if (currentPlayIndex <= 0)
        {
            backwardButton.interactable = false;
        }
        forwardButton.interactable = true;
    }

    public void MuteUnmuteAudio()
    {
        audioPlayer.mute = !audioPlayer.mute;
        if (audioPlayer.mute)
        {
            voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_mute_icon");
        }
        else
        {
            voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_unmute_icon");
        }
    }

    public void PausePlayAudio()
    {
        if (audioPlayer.isPlaying)
        {
            audioPlayer.Pause();
            playAudio = false;
            playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        }
        else
        {
            audioPlayer.Play();
            playAudio = true;
            playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/pause_icon");
        }
    }
}
