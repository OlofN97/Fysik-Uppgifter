using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Box3 : MonoBehaviour
{
    public InputField field_friction;
    public InputField field_startvel;

    float friction_coefficient;

    float delta = 0.1f;

    float mass;
    float acceleration;
    float start_velocity;
    float[] velocity, time;
    float[] y, z;
    bool moving;

    float force_normal;
    float force_gravity;
    float force_friction;
    float angle;

    int N = 101;

    void Start()
    {
        mass = 30; //kg
        angle = 30 * Mathf.Deg2Rad;
        moving = false;

        velocity = new float[N];
        time = new float[N];
        y = new float[N];
        z = new float[N];
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            for (int i = 0; i < N; i++)
            {
                time[i] = i * delta; //time.deltaTime? Fråga imorgon på lektion
                if (i == 0)
                {
                    velocity[i] = start_velocity;
                    y[i] = 0;
                    z[i] = 0;
                }
                else
                {
                    velocity[i] = velocity[i - 1] + acceleration * Time.deltaTime;
                    z[i] = z[i - 1] + 0.5f * (velocity[i - 1] + velocity[i]) * Time.deltaTime * Mathf.Sin(angle);
                    y[i] = y[i - 1] + 0.5f * (velocity[i - 1] * +velocity[i]) * Time.deltaTime * Mathf.Cos(angle);

                    Debug.Log(gameObject.transform.position);
                }
                gameObject.transform.position += new Vector3(0, -y[i], z[i]);
            }
        }
    }

    public void SetValues()
    {
        start_velocity = CheckIfInputCorrect(field_startvel, start_velocity);
        friction_coefficient = CheckIfInputCorrect(field_friction, friction_coefficient);
        if (start_velocity != -1 && friction_coefficient != -1)
        {
            force_gravity = 9.82f * mass;
            force_normal = force_gravity * Mathf.Cos(angle);
            force_friction = -(friction_coefficient * force_normal) * Mathf.Sin(angle);

            acceleration = 9.82f * (Mathf.Sin(angle) - friction_coefficient * Mathf.Cos(angle));
            //if (acceleration < 0)
            //    acceleration = 0;
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
