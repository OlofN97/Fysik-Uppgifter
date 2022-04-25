using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject car;
    [SerializeField] private float staticFriciton;
    private float radius = 30;
    private bool running;
    private bool onRoad;
    [SerializeField] private float speed;
    private float maxSpeed;
    private const float gravitation = 9.82f;


    private float circumference;
    private float distanceTraveled;
    private float angleTraveled;


    void Start()
    {
        running = false;
    }

    void Update()
    {
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
        if (onRoad)
        {
            //Kod för hur den ska köra här
        }
    }
    private void UpdateRotation()
    {
        Vector3 rotation = new Vector3(0, 0, (speed * Time.deltaTime / circumference) * 360);
        car.transform.Rotate(rotation);
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
        car = Instantiate(car, transform.position + new Vector3(radius, 0, 0), Quaternion.identity);
        running = true;
        onRoad = true;
        circumference = Mathf.PI * 2 * radius;
        SetMaxSpeed();
    }
}
