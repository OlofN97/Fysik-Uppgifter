using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Box2 : MonoBehaviour
{
    public InputField field_friction;
    public InputField field_startvel;

    float friction_coefficient;

    float mass;
    float angle;
    float acceleration;
    float start_velocity;
    float velocity;
    bool moving;

    float force_normal;
    float force_gravity;
    float force_friction;

    void Start()
    {
        mass = 30; //kg
        angle = 30 * Mathf.Deg2Rad;
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            velocity = start_velocity + acceleration * Time.deltaTime;
            float posChange = (start_velocity + velocity) / 2 * Time.deltaTime;
            gameObject.transform.position += new Vector3(0, -posChange * Mathf.Sin(30 * Mathf.Deg2Rad), posChange * Mathf.Cos(30 * Mathf.Deg2Rad));
        }
        else
            velocity = 0;
    }

    public void SetValues()
    {
        start_velocity = CheckIfInputCorrect(field_startvel, start_velocity);
        friction_coefficient = CheckIfInputCorrect(field_friction, friction_coefficient);
        if(start_velocity != -1 && friction_coefficient != -1)
        {
            force_gravity = 9.82f * mass;
            force_normal = force_gravity * Mathf.Sin(30 * Mathf.Deg2Rad);
            force_friction = -(friction_coefficient * force_normal) * Mathf.Cos(30*Mathf.Deg2Rad);

            acceleration = (force_normal + force_gravity + force_friction) / mass; //A = FRES / M
            //if (acceleration < 0)
            //    acceleration = 0;
            if (velocity < 0)
                velocity = 0;
            moving = true;
        }
    }

    float CheckIfInputCorrect(InputField ifield, float output)
    {
        bool isNumber = float.TryParse(ifield.text, out output);
        if (isNumber && output >= 0)
            return output;
        else
        {
            Debug.Log("Error!");
            if (!isNumber)
                Debug.Log("One or more inputs is not a number. Please insert a valid value.");
            if (output < 0)
                Debug.Log("Input cannot be a negative number. Please insert a positive number or zero.");
        }
        return -1;
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
