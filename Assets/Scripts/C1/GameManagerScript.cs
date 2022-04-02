using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] Text TextRadius;
    [SerializeField] Text TextFriction;
    [SerializeField] Text TextSpeed;
    [SerializeField] Slider SliderSpeed;
    [SerializeField] Text TextStatus;
    [SerializeField] Text TextDistance;

    [Range(0,1)]
    [SerializeField] float friction;
    [SerializeField] float C1Radius;



    private const float gravity = 9.82f;

    // Centripetal force = Mv^2 / r

    CarScript car;

    void Start()
    {
        car = new CarScript(friction, C1Radius, gravity);
    }

    void Update()
    {
        UpdateUI();
        if (car.Running)
        {
            UpdateCar();
        }      
    }

    private void UpdateCar()
    {
        car.Update(SliderSpeed.value, Time.deltaTime);


    }

    private void UpdateUI()
    {
        
    }   
}
