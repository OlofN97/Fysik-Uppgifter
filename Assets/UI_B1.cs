using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_B1 : MonoBehaviour
{
    public GameObject box;
    public Text veltext;
    public Text pos_text;
    public Text acc_text;
    Vector3 boxStartPos;
    void Start()
    {
        boxStartPos = box.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        veltext.text = $"Velocity = {(int)(box.GetComponent<Box3>().velocitynew)} m/s";
        pos_text.text = $"Change in position = [" +
            $"{(int)(box.transform.position.z-boxStartPos.z)}, " +
            $"{(int)(box.transform.position.y - boxStartPos.y)}]";
        acc_text.text = $"Acceleration = {box.GetComponent<Box3>().accelerationnew} m/s^2";
    }
}
