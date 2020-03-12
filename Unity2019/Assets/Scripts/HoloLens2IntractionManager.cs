using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;
using UnityEngine.XR;

using UnityEngine.XR.InteractionSubsystems;
using UnityEngine.XR.Management;
using UnityEngine.XR.WindowsMR;

public class HoloLens2IntractionManager : MonoBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform rightChildHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform leftChildHand;

    // Start is called before the first frame update
    void Start()
    {
        var manager=XRGeneralSettings.Instance.Manager;
        if (manager==null)
        {
            Debug.LogError("No Manager");
        }
        else
        {
            var loader = manager.activeLoader;
            if (loader == null)
            {
                Debug.LogError("No Loader");
            }
        }

        var gesture = GetComponent<WindowsMRGestures>();
        gesture.onTappedChanged += Gesture_onTappedChanged;
    }

    private void Gesture_onTappedChanged(WindowsMRTappedGestureEvent obj)
    {
        if (obj.state == GestureState.Started)
        {
            rightChildHand.localScale = Vector3.one * 0.5f;
            leftChildHand.localScale = Vector3.one * 0.5f;
        }
        else if (obj.state == GestureState.Completed || obj.state == GestureState.Canceled)
        {
            rightChildHand.localScale = Vector3.one;
            leftChildHand.localScale = Vector3.one;
        }
    }

    private void Update()
    {
        //var inputsubsystem = XRGeneralSettings.Instance.Manager.activeLoader.GetLoadedSubsystem<WindowsMRGestureSubsystem>();
    }
}
