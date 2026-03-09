using UnityEngine;

public class Fih : MonoBehaviour
{
// This defines what the options ARE
    public enum FihType 
    { 
        NormalFih, 
        BigFih, 
        DartFih
    }

    // This creates the actual dropdown variable in the Inspector
    [Header("Fih Type")]
    public FihType selectedFish;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
