using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Box : MonoBehaviour
{
    public float input;
    public InputField ifield;
    public InputField ifield_vel_start;
    float kinetic_friction;
    float stationary_friction;

    float force_normal;
    float force_gravity;
    float force_friction;

    float acceleration;
    float mass = 30; //kg

    float velocity;
    float vel_start;
    bool moving = false;
    

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            velocity = vel_start + acceleration * Time.deltaTime;
            gameObject.transform.position += new Vector3(0, -velocity * Mathf.Sin(30 * Mathf.Deg2Rad), velocity * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        else
            velocity = 0;
    }
    
    void SetValues()
    {
        kinetic_friction = input;

        force_gravity = 9.82f * mass;
        force_normal = force_gravity * Mathf.Sin(30 * Mathf.Deg2Rad);
        force_friction = -(kinetic_friction* force_normal);

        acceleration = (force_normal + force_gravity + force_friction) / mass; //A = FRES / M
        if (acceleration< 0)
            acceleration = 0;
    }

    public void SetBoth()
    {
        SetInput(ifield, input);
        SetInput(ifield_vel_start, vel_start);
    }

    public void SetInput(InputField i, float output)
    {
        bool isNumber = float.TryParse(i.text, out output);

        if (isNumber && output >=0)
        {
            SetValues();
            moving = true;
        }
        else
        {
            Debug.Log("Error!");
            if (!isNumber)
                Debug.Log("Input is not a number. Please insert a valid value.");
            if (input < 0)
                Debug.Log("Input cannot be a negative number. Please insert a positive number or zero.");
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
