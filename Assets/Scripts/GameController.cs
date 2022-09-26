using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] Vector2 stick;
    [SerializeField] int buttonY;
    [SerializeField] int buttonX;
    [SerializeField] int buttonA;
    [SerializeField] int buttonB;
    [SerializeField] TMP_InputField serverAddress;
    [SerializeField] TMP_InputField serverPort;
    bool canSendData;

    void Start()
    {
        stick = new Vector2(0, 0);
        buttonY = 0;
        buttonX = 0;
        buttonA = 0;
        buttonB = 0;
        canSendData = true;
    }
    private void SendDataToServer()
    {
        if (canSendData)
        {
            canSendData = false;
            StartCoroutine(SendData());
        }
    }

    private IEnumerator SendData()
    {
        Debug.Log("Stick X = " + stick.x);
        Debug.Log("Stick Y = " + stick.y);
        WWWForm form = new WWWForm();
        form.AddField("stickX", (int)(stick.x * 10000000));
        form.AddField("stickY", (int)(stick.y * 10000000));
        form.AddField("botaoY", buttonY.ToString());
        form.AddField("botaoX", buttonX.ToString());
        form.AddField("botaoA", buttonA.ToString());
        form.AddField("botaoB", buttonB.ToString());

        string address = serverAddress.text;
        string port = serverPort.text;
        if (address == "")
        {
            address = "localhost";
        }
        if (port == "")
        {
            port = "80";
        }
        string finalDestination = $"http://{address}:{port}";

        using (UnityWebRequest www = UnityWebRequest.Post(finalDestination, form))
        {
            yield return www.SendWebRequest();
            canSendData = true;

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
    public void OnAnalog(InputValue value)
    {
        stick = value.Get<Vector2>();
        SendDataToServer();
    }
    public void OnButtonY(InputValue value)
    {
        if (value.isPressed)
        {
            buttonY = 1;
        }
        else
        {
            buttonY = 0;
        }
        SendDataToServer();
    }
    public void OnButtonX(InputValue value)
    {
        if (value.isPressed)
        {
            buttonX = 1;
        }
        else
        {
            buttonX = 0;
        }
        SendDataToServer();
    }
    public void OnButtonA(InputValue value)
    {
        if (value.isPressed)
        {
            buttonA = 1;
        }
        else
        {
            buttonA = 0;
        }
        SendDataToServer();
    }
    public void OnButtonB(InputValue value)
    {
        if (value.isPressed)
        {
            buttonB = 1;
        }
        else
        {
            buttonB = 0;
        }
        SendDataToServer();
    }
}
