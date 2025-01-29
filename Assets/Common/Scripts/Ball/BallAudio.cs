using UnityEngine;

public class BallAudio : MonoBehaviour
{
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        _audioSource.Play();
    }   
}