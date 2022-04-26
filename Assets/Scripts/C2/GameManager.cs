using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private float staticFriciton;
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;

    private float radius = 30;
    private bool running;
    private bool onRoad;
    private float maxSpeed;
    private const float gravitation = 9.82f;

    private float circumference;
    private float distanceTraveled;
    private float angleTraveled;

    [Header("UI")]
    [SerializeField] private Text speedText;
    [SerializeField] private Slider speedInput;
    [SerializeField] private InputField radieInput;
    [SerializeField] private Text frictionText;
  

    void Start()
    {
        running = false;
        frictionText.text = "Friction: " + staticFriciton;
        speedText.text = "Speed: " + speedInput.value;
    }

    void Update()
    {

        speed = speedInput.value;

        if (running)
        {
            UpdateMovement();
            UpdateRotation();
        }
    }

    private void UpdateMovement()
    {
        if (speed > maxSpeed) onRoad = false;

        if (onRoad)
        {
            distanceTraveled += speed * Time.deltaTime;
            angleTraveled = (distanceTraveled / circumference) * 360 * Mathf.Deg2Rad;
            car.transform.position = new Vector3(Mathf.Cos(angleTraveled) * radius, Mathf.Sin(angleTraveled) * radius, 0);
        }
        if (!onRoad)
        {
            car.transform.position += car.transform.up *speed *Time.deltaTime;
        }
    }
    private void UpdateRotation()
    {
        if (onRoad)
        {
            Vector3 rotation = new Vector3(0, 0, (speed * Time.deltaTime / circumference) * 360);
            car.transform.Rotate(rotation);
        }
    }

    private void SetMaxSpeed()
    {
        /*Maximum speed that the car can have in the turn without slipping
        * maxv = square(r*f*g)
        *
        */
        maxSpeed = Mathf.Sqrt(radius * staticFriciton * gravitation);
    }


    public void StartButton()
    {
        GetInputs();
        SetMaxSpeed();

        car = Instantiate(car, transform.position + new Vector3(radius, 0, 0), Quaternion.identity);
        running = true;
        onRoad = true;
        circumference = Mathf.PI * 2 * radius;

        cam.orthographicSize = radius;

    }

    private void GetInputs()
    {
        radius = float.Parse(radieInput.text);
        speed = speedInput.value;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("C2");
    }

    public void OnSpeedChange()
    {
        speedText.text = "Speed: " + speedInput.value;
    }
    
}
