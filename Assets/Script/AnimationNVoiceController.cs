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

    public Animator activeAnimator;
    public List<Animator> animators;
    public List<string> HDDAnimName;
    public List<string> USBAnimName;
    public List<string> OPSAnimName;

    public Button forwardButton;
    public Button backwardButton;
    public Button playButton;
    public Button voiceControlButton;
    public Button resetButton;

    private List<AudioClip> currentClips;
    private List<string> currentAnim;
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
                currentAnim = HDDAnimName;
                activeAnimator = animators[0];
                break;

            case 2:
                currentClips = USBClips;
                currentAnim = USBAnimName;
                activeAnimator = animators[1];
                break;

            case 3:
                currentClips = OPSClips;
                currentAnim = OPSAnimName;
                activeAnimator = animators[2];
                break;
        }

        audioPlayer.clip = currentClips[0];
        audioPlayer.mute = false;
        activeAnimator.Play(currentAnim[0]);
        activeAnimator.enabled = false;
        voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_unmute_icon");
        forwardButton.interactable = true;
        backwardButton.interactable = false;
        playButton.interactable = true;
        resetButton.interactable = true;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        voiceControlButton.interactable = true;
    }

    public void DisablePlayBack()
    {
        currentClips = null;
        currentAnim = null;
        activeAnimator = null;
        audioPlayer.clip = null;
        audioPlayer.mute = false;
        voiceControlButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/volume_unmute_icon");
        forwardButton.interactable = false;
        backwardButton.interactable = false;
        playButton.interactable = false;
        resetButton.interactable = false;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        voiceControlButton.interactable = false;
    }

    public void ResetPlayBack()
    {
        playAudio = false;
        playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        audioPlayer.Stop();
        activeAnimator.Play(currentAnim[0]);
        activeAnimator.enabled = false;
        audioPlayer.clip = currentClips[0];
        forwardButton.interactable = true;
        backwardButton.interactable = false;
        activeAnimator.transform.localScale = activeAnimator.transform.name.Equals("FlashMemory") ? new Vector3(0.02f, 0.02f, 0.02f) : new Vector3(0.02f, 0.02f, 0.02f);
        activeAnimator.GetComponent<Transform>().localRotation = Quaternion.Euler(0f, 0f, 0f);
        activeAnimator.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        activeAnimator.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void NextClip()
    {
        if (audioPlayer.isPlaying) audioPlayer.Stop();
        int currentPlayIndex = currentClips.IndexOf(audioPlayer.clip) + 1;
        audioPlayer.clip = currentClips[currentPlayIndex];
        audioPlayer.Play();
        activeAnimator.Play(currentAnim[currentPlayIndex]);
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
        int currentPlayIndex = currentClips.IndexOf(audioPlayer.clip) - 1;
        audioPlayer.clip = currentClips[currentPlayIndex];
        audioPlayer.Play();
        activeAnimator.Play(currentAnim[currentPlayIndex]);
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
            activeAnimator.enabled = false;
            playAudio = false;
            playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/play_icon");
        }
        else
        {
            audioPlayer.Play();
            activeAnimator.enabled = true;
            playAudio = true;
            playButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/pause_icon");
        }
    }
}
