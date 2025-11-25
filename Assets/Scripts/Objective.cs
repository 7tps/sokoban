using UnityEngine;

public class Objective : MonoBehaviour
{
    
    public SpriteRenderer sr;

    public Sprite inactive;
    public Sprite active;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = inactive;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        sr.sprite = active;
    }

    public void Deactivate()
    {
        sr.sprite = inactive;
    }
}
