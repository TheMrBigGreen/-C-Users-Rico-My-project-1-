using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    
    private PlayerController playerController;
    private ScoreManager scoreManager;
    private Pin[] pins;

    // Start is called before the first frame update
    void Start()
    {

    }
    

    // Update is called once per frame
    void Update()
    {
        CalculateFallenPins();
    }

    public void SetNextThrow()
    {

        Invoke(nameof(NextThrow), 3.0f);
    }

    void NextThrow()
    {
        if(scoreManager.currentFrame == 0)
        {
            Debug.Log($"Game over {scoreManager.CalculateTotalScore()}");
        }
        else
        {
            Debug.Log($"Frame: {scoreManager.currentFrame}, Throw: {scoreManager.currentThrow}");
            scoreManager.SetFrameScore(CalculateFallenPins());
            Debug.Log($"Current Score: {scoreManager.CalculateTotalScore()}");

            //Get the ball to the start position for throwing
            playerController.StartThrow();
        }
    }

    public int CalculateFallenPins()
    {
        int count = 0;
        foreach (Pin pin in pins)

        if(pin.isFallen)
        { 
            count++;
            pin.gameObject.SetActive(false);
        }

        Debug.Log("Total Fallen Pins " + count);
        return count;
    }

    public void ResetAllPins()
    {
        foreach (Pin pin in pins)
        {
            pin.ResetPin();
        }
    }


}
