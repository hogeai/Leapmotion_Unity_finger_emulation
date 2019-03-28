using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class SimpleLeapMotion : MonoBehaviour
{
    // Start is called before the first frame update
    Controller controller;
    public GameObject[] FingerObjects;

    void Start()
    {
        controller = new Controller();

    }

    void OnApplicationQuit()
    {
        if (controller != null)
        {
            controller.StopConnection();
            controller.Dispose();
            controller = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Frame frame = controller.Frame();
        List<Hand> hands = frame.Hands;

        int f_cnt = hands.Count * 5;
        int f_hidden_cnt = 2 * 5 - f_cnt;
        int h_cnt = 2;
        int ff_cnt = 0;

        for (int idxFinger = 0; idxFinger < f_hidden_cnt; ++idxFinger)
        {
            var fingerObj = FingerObjects[ff_cnt++];
            fingerObj.SetActive(false);
        }

        for (int i = 0; i < h_cnt; i++)
        {
            List<Finger> fingers = hands[i].Fingers;
            for (int idxFinger = 0; idxFinger < fingers.Count; ++idxFinger)
            {
                Finger finger = fingers[idxFinger];
                Vector fingerPos = finger.TipPosition;
                Color set_collor = new Color(0, 0, 0);

                if (finger.Type == Finger.FingerType.TYPE_THUMB) { set_collor = new Color(255, 0, 0); }
                else if (finger.Type == Finger.FingerType.TYPE_INDEX) { set_collor = new Color(0, 255, 0); }
                else if (finger.Type == Finger.FingerType.TYPE_MIDDLE) { set_collor = new Color(0, 0, 255); }
                else if (finger.Type == Finger.FingerType.TYPE_RING) { set_collor = new Color(255, 255, 0); }
                else if (finger.Type == Finger.FingerType.TYPE_PINKY) { set_collor = new Color(0, 255, 255); }
                else { set_collor = new Color(255, 255, 255); }

                var fingerObj = FingerObjects[ff_cnt++];
                fingerObj.transform.position = UnityVectorExtension.ToVector3(fingerPos) / 10;
                fingerObj.GetComponent<Renderer>().material.color = set_collor;
                fingerObj.SetActive(true);
            }

        }
    }
}
