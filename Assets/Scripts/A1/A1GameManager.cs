using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A1GameManager : MonoBehaviour
{
    [SerializeField] GameObject boll;
    [SerializeField] Button buttonStart;

    [SerializeField] InputField inputX;
    [SerializeField] InputField inputY;
    [SerializeField] InputField inputHastighet;
    [SerializeField] InputField inputVinkel;

    [SerializeField] Text textXPosition;
    [SerializeField] Text textYPosition;
    [SerializeField] Text textHastighetX;
    [SerializeField] Text textHastighetY;

    private bool ballActive;
    float xPosition;
    float yPosition;
    float hastighet;
    float vinkel;

    void Start()
    {
        ballActive = false;
    }

    void Update()
    {
        if (ballActive)
        {




            if(yPosition <= 0)
            {
                ballActive = false;
            }
        }
    }

    public void ButtonPressed()
    {
        ballActive = true;
        xPosition = float.Parse(inputX.text);
        xPosition = float.Parse(inputY.text);
        hastighet = float.Parse(inputHastighet.text);
        vinkel = float.Parse(inputVinkel.text);

        boll.transform.position = new Vector3(xPosition, yPosition);   
    }
}
