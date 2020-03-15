using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class HoloLens2IntractionManager : MonoBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform rightChildHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform leftChildHand;

    // Start is called before the first frame update
    void Start()
    {
        rightHand.gameObject.SetActive(false);
        leftHand.gameObject.SetActive(false);

        InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;
        InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
        InteractionManager.InteractionSourcePressed += InteractionManager_InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionManager_InteractionSourceReleased;
        InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
    }

    private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        // Update
        var handedness = obj.state.source.handedness;
        Vector3 pos;
        Quaternion rot;
        if (obj.state.sourcePose.TryGetPosition(out pos))
        {
            if (obj.state.sourcePose.TryGetRotation(out rot))
            {
                if (handedness == InteractionSourceHandedness.Right)
                {
                    rightHand.SetPositionAndRotation(pos, rot);
                }
                else if (handedness == InteractionSourceHandedness.Left)
                {
                    leftHand.SetPositionAndRotation(pos, rot);
                }
            }
        }
    }

    private void InteractionManager_InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        // Release
        var handedness = obj.state.source.handedness;
        if (handedness == InteractionSourceHandedness.Right)
        {
            rightChildHand.localScale = Vector3.one;
        }
        else if (handedness == InteractionSourceHandedness.Left)
        {
            leftChildHand.localScale = Vector3.one;
        }
    }

    private void InteractionManager_InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        // Press
        var handedness = obj.state.source.handedness;
        if (handedness == InteractionSourceHandedness.Right)
        {
            rightChildHand.localScale = Vector3.one * 0.5f;
        }
        else if (handedness == InteractionSourceHandedness.Left)
        {
            leftChildHand.localScale = Vector3.one * 0.5f;
        }
    }

    private void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
    {
        // Lost
        var handedness = obj.state.source.handedness;
        if (handedness == InteractionSourceHandedness.Right)
        {
            rightHand.gameObject.SetActive(false);
        }
        else if (handedness == InteractionSourceHandedness.Left)
        {
            leftHand.gameObject.SetActive(false);
        }
    }

    private void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
    {
        // Detect
        var handedness = obj.state.source.handedness;
        if (handedness==InteractionSourceHandedness.Right)
        {
            rightHand.gameObject.SetActive(true);
        }
        else if (handedness==InteractionSourceHandedness.Left)
        {
            leftHand.gameObject.SetActive(true);
        }
    }
}
