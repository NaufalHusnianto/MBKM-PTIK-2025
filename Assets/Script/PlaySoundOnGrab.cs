using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable), typeof(AudioSource))]
public class PlaySoundOnGrab : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    void Start()
    {
        // Ambil komponen XRGrabInteractable dan AudioSource
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        // Tambahkan event listener saat objek di-grab
        grabInteractable.selectEntered.AddListener(PlayAudioOnGrab);
    }

    private void PlayAudioOnGrab(SelectEnterEventArgs args)
    {
        // Pastikan audio tidak sedang diputar, lalu mainkan
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnDestroy()
    {
        // Hapus listener saat objek dihancurkan (opsional, untuk menghindari memory leak)
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(PlayAudioOnGrab);
        }
    }
}