using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdCage : MonoBehaviour
{
    public AudioClip destroySound; // Geluid wanneer de cage verdwijnt
    private bool isDestroyed = false;

    public Animator characterAnimator;  // Verwijzing naar de Animator van het character in de cage

    void OnCollisionEnter(Collision other)
    {
        if (isDestroyed) return; // Zorgt dat het maar 1 keer gebeurt

        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player raakt de bird cage. Cage verwijderen en geluid afspelen.");

            // Verwijder de cage direct
            Destroy(gameObject);

            // Speel het geluid af
            PlayDestroySound();

            // Zet de parameter 'IsReleased' op true om de release animatie af te spelen
            if (characterAnimator != null)
            {
                characterAnimator.SetBool("IsReleased", true); // Start de release animatie
            }
        }
    }

    void PlayDestroySound()
    {
        // Maak een tijdelijke AudioSource aan
        GameObject soundObject = new GameObject("DestroySound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();

        // Zet de AudioClip en speel het geluid af
        audioSource.clip = destroySound;
        audioSource.Play();

        // Verwijder het geluid-object nadat het geluid klaar is
        Destroy(soundObject, destroySound.length);
    }
}
