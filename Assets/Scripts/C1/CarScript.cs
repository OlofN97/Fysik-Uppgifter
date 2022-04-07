using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript 
{
    float speed;
    float friction;
    float radius;
    float maxSpeed;
    float gravitation;
    float distanceTraveled;
    float maxdistance;

    bool running;
    bool isOnRoad;
    public bool Running { get { return running; } }
    public bool IsOnRoad { get { return isOnRoad; } }
    public float DistanceTraveled { get { return distanceTraveled; } }

    public CarScript(float friction, float radius, float gravitation)
    {
        this.friction = friction;
        this.radius = radius;
        this.gravitation = gravitation;
    }
    

    public void Update(float speed, float deltaTime)
    {
        this.speed = speed;
        SetMaxSpeed();

        if(speed > maxSpeed) 
        {
            isOnRoad = false;
        }

        distanceTraveled += speed * deltaTime;
    }

    private void SetMaxSpeed()
    {
        /*Maximum speed that the car can have in the turn without slipping
        * maxv = square(r*f*g)
        *
        */
        maxSpeed = Mathf.Sqrt(radius * friction * gravitation); 
    }

    public void StartCar(float radius)
    {
        this.radius = radius;
        isOnRoad = true;
        running = true;
    }

    
}
