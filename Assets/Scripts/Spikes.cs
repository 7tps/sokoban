using UnityEngine;

public class Spikes : MonoBehaviour
{

    public SpriteRenderer sr;

    public Sprite raisedSpikes;
    public Sprite loweredSpikes;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = raisedSpikes;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RaiseSpikes()
    {
        sr.sprite = raisedSpikes;
    }

    public void LowerSpikes()
    {
        sr.sprite = loweredSpikes;
        
    }
}
