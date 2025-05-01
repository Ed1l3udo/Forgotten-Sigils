using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceMainTheme;
    void Start()
    {
        audioSourceMainTheme.loop = true;
        audioSourceMainTheme.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
