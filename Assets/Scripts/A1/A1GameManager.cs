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
    private float xPosition;
    private float yPosition;
    private float startHastighet;
    private float vinkel;
    private float gravitation;
    private float timeStart;
    private float Vx;
    private float Vy;
    void Start()
    {
        ballActive = false;
        gravitation = -9.82f;
        timeStart = 0;
        boll.GetComponent<TrailRenderer>().enabled = false;
    }

    void Update()
    {
        if (ballActive)
        {
            UpdatePosition();
            UpdateUi();


            if (boll.transform.position.y <= 0)
            {
                ballActive = false;
                boll.GetComponent<TrailRenderer>().enabled = false;
            }
        }
    }

    private void UpdatePosition()
    {
        float t = Time.realtimeSinceStartup - timeStart;
        float dt = Time.deltaTime;
        //Vx = vx0 + ax*t = V * cos(x) 
        //Vy = Vy0 + ay*t = V * sin(y) + ay * t
        Vx = startHastighet * Mathf.Cos(Mathf.Deg2Rad * vinkel);
        Vy = startHastighet * Mathf.Sin(Mathf.Deg2Rad * vinkel) + gravitation * t;

        //boll.transform.position = boll.transform.position + new Vector3(Vx * Time.deltaTime, Vy * Time.deltaTime); 

        //Fysiskt korrekt (inkluderar acceleration i formeln för positionsändring i Y-led.)
        boll.transform.position += new Vector3(Vx * dt, Vy * dt + 0.5f * (gravitation * dt*dt));
    }
    private void UpdateUi()
    {
        textHastighetX.text = "Hastighet i X led: " + Vx.ToString() + " m/s";
        textHastighetY.text = "Hastighet i Y led: " + Vy.ToString() + " m/s";
        textXPosition.text = "X: " + boll.transform.position.x.ToString();
        textYPosition.text = "Y: " + boll.transform.position.y.ToString();
    }

    public void StartPressed()
    {
        ballActive = true;
        xPosition = float.Parse(inputX.text);
        yPosition = float.Parse(inputY.text);
        startHastighet = float.Parse(inputHastighet.text);
        vinkel = float.Parse(inputVinkel.text);
        boll.transform.position = new Vector3(xPosition, yPosition);
        timeStart = Time.realtimeSinceStartup;
        boll.GetComponent<TrailRenderer>().enabled = true;
    }

    public void ResetPressed()
    {
        ballActive = false;
        boll.transform.position = Vector3.zero;
        
    }
}
