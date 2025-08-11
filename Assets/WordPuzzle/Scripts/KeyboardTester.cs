using UnityEngine;

public class KeyboardTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeyboardKey.onKeyPressed += DebugLetter;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DebugLetter(char letter)
    {
        //Debug.Log(letter);
    }
}
