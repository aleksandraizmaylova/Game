using UnityEngine;
using UnityEngine.Video;

public class Fragment : MonoBehaviour
{
    public GameObject cutscene;
    private Mirror Mirror;
    public int fragmentNumber;

    private VideoPlayer videoCutscene;

    private void Start()
    {
        Mirror = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().mirror;
        videoCutscene = cutscene.GetComponent<VideoPlayer>();
        videoCutscene.loopPointReached += OnVideoFinished;
        videoCutscene.isLooping = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cutscene.SetActive(true);
            videoCutscene.Play();
            Mirror.ActivateFragment(fragmentNumber);
            Destroy(gameObject);
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        cutscene.SetActive(false);
    }
}