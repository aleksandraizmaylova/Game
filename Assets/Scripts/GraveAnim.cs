using UnityEngine;

public class GraveAnim : MonoBehaviour
{
    public GameObject fragment;
    public GameObject grave;
    
    public void OnAnimationEnding()
    {
        fragment.SetActive(true);
        grave.SetActive(false);
    }
}
