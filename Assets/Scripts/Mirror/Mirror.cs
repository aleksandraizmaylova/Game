using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject[] fragments;
    public bool full;
    private int counter;
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
        counter++;

        Debug.Log($"counter = {counter}, full = {full}");
        
        if (counter == 4)
        {
            Debug.Log("зеркало заполнено");
            full = true;
        }
    }
}
