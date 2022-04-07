using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] Text TextRadius1;
    [SerializeField] Text TextRadius2;
    [SerializeField] Text TextFriction;
    [SerializeField] Text TextSpeed;
    [SerializeField] Slider SliderSpeed;
    [SerializeField] Text TextStatus;
    [SerializeField] Text TextDistance;
    [SerializeField] Toggle toogleRadius1;
    [SerializeField] Toggle toogleRadius2;

    [Range(0,1)]
    [SerializeField] float friction;
    [SerializeField] float radius1;
    [SerializeField] float radius2;

    private float currentRadius;
    private const float gravity = 9.82f;

    // Centripetal force = Mv^2 / r

    CarScript car;

    void Start()
    {
        car = new CarScript(friction, currentRadius, gravity);
        TextRadius1.text = "Radius 1: " + radius1.ToString();
        TextRadius2.text = "Radius 2: " + radius2.ToString();
        TextFriction.text = "Friction: " + friction.ToString();
        
        
    }

    void Update()
    {
        UpdateUI();
        if (car.Running)
        {
            car.Update(SliderSpeed.value, Time.deltaTime);
        }      
    }

    private void UpdateUI()
    {     
        TextSpeed.text ="Speed (m/s): " + SliderSpeed.value.ToString();
        if (car.Running && car.IsOnRoad)
            TextStatus.text = "Status: Car is still on road";
        else if (car.Running && !car.IsOnRoad)
            TextStatus.text = "Status: Car is off road";
        else if (!car.Running)
            TextStatus.text = "Status: Car is not running";
    }

    public void StartCar()
    {
        car.StartCar(currentRadius);
    }

    public void ToogleRadius1()
    {
        currentRadius = radius1;
        toogleRadius2.isOn = false;
    }
    public void ToogleRadius2()
    {
        currentRadius = radius2;
        toogleRadius1.isOn = false;
    }  
}
