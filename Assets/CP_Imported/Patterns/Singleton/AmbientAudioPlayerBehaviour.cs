using UnityEngine;

public class AmbientAudioPlayerBehaviour : MonoBehaviour {
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public void FadeNewMusic(AudioClip clip)
    {
        // TODO
    }
}