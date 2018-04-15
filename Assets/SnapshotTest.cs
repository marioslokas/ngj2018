using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTest : MonoBehaviour
{
    public AudioMixerSnapshot Slow;
    public AudioMixerSnapshot Fast;

    bool isFast = false;

    public float TransitionTiming = 2;
}