using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject[] fragments;
    void Start()
    {
        foreach (var fragment in fragments)
        {
            fragment.SetActive(false);
        }
    }

    public void ActivateFragment(int fragmentNumber)
    {
        fragments[fragmentNumber].SetActive(true);
    }
}
