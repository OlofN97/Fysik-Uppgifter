using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A2Ball : MonoBehaviour
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
    private Vector2 position;
    private float startHastighet;
    private float vinkel;
    private float gravitation;
    private float timeStart;
    private float vx, vy;
    private float xdir, ydir;

    void Start()
    {      
        ballActive = false;
        gravitation = -9.82f;
        timeStart = 0;
        xdir = 1;
        ydir = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballActive)
        {
            UpdatePosition();
            UpdateUi();
        }
    }

    private void UpdatePosition()
    {
        float t = Time.realtimeSinceStartup - timeStart;
        float dt = Time.deltaTime;
        //vx = startHastighet * Mathf.Cos(Mathf.Deg2Rad * vinkel) * xdir;
        //vy = (startHastighet * Mathf.Sin(Mathf.Deg2Rad * vinkel) + gravitation * t)*ydir;
        //vx *= xdir;
        //vy *= ydir;

        vy += gravitation * Time.deltaTime;

        //boll.transform.position = boll.transform.position + new Vector3(Vx * Time.deltaTime, Vy * Time.deltaTime); 

        //Fysiskt korrekt (inkluderar acceleration i formeln för positionsändring i Y-led.)
        transform.position += new Vector3(vx * dt, vy * dt );
    }

    public void StartPressed()
    {
        ballActive = true;
        position.x = float.Parse(inputX.text);
        position.y = float.Parse(inputY.text);
        startHastighet = float.Parse(inputHastighet.text);
        vinkel = float.Parse(inputVinkel.text);
        transform.position = new Vector3(position.x, position.y);
        timeStart = Time.realtimeSinceStartup;

        vx = startHastighet * (Mathf.Cos(Mathf.Deg2Rad * vinkel));
        vy = startHastighet * (Mathf.Sin(Mathf.Deg2Rad * vinkel));
    }

    public void ResetPressed()
    {
        ballActive = false;
        transform.position = Vector3.zero;
        vx = 0;
        vy = 0;
        textHastighetX.text = "Hastighet i X led: 0 m/s";
        textHastighetY.text = "Hastighet i Y led: 0 m/s";
        textXPosition.text = "X: 0";
        textYPosition.text = "Y: 0";

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Wall w = collision.gameObject.GetComponent<Wall>();
        int wallpos = w.CheckWall();

        switch (wallpos)
        {
            case 1:
                vy *= -1;
                break;
            case 2:
                vy *= -1;
                break;
            case 3:
                vx *= -1;
                break;
            case 4:
                Debug.Log("x");
                vx *= -1;
                break;
            default:
                return;
        }

        float rec = w.checkRestitutionCoefficient();
        vy *= rec;
        vx *= rec;
    }

    private void UpdateUi()
    {
        textHastighetX.text = "Hastighet i X led: " + ((int)Mathf.Abs(vx)).ToString() + " m/s";
        textHastighetY.text = "Hastighet i Y led: " + ((int)Mathf.Abs(vy)).ToString() + " m/s";
        textXPosition.text = "X: " + ((int)boll.transform.position.x).ToString();
        textYPosition.text = "Y: " + ((int)boll.transform.position.y).ToString();
    }
}
