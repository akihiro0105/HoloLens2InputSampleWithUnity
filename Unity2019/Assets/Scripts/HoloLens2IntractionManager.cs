using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if WINDOWS_UWP
using Windows.UI.Input.Spatial;
#endif

public class HoloLens2IntractionManager : MonoBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform rightChildHand;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform leftChildHand;

    // Start is called before the first frame update
    void Start()
    {
#if WINDOWS_UWP
        SpatialInteractionManager spatialInteraction = null;
        UnityEngine.WSA.Application.InvokeOnUIThread(() =>
        {
            spatialInteraction = SpatialInteractionManager.GetForCurrentView();
        }, true);
        spatialInteraction.SourcePressed += SpatialInteraction_SourcePressed;
        spatialInteraction.SourceReleased += SpatialInteraction_SourceReleased;
#endif
    }

#if WINDOWS_UWP

    private void SpatialInteraction_SourceReleased(SpatialInteractionManager sender, SpatialInteractionSourceEventArgs args)
    {
        var item = args.State;
        UnityEngine.WSA.Application.InvokeOnAppThread(() =>
        {
            if (item.Source.Handedness == SpatialInteractionSourceHandedness.Right)
            {
                rightChildHand.localScale = Vector3.one;
            }
            else if (item.Source.Handedness == SpatialInteractionSourceHandedness.Left)
            {
                leftChildHand.localScale = Vector3.one;
            }
        }, false);
    }

    private void SpatialInteraction_SourcePressed(SpatialInteractionManager sender, SpatialInteractionSourceEventArgs args)
    {
        var item = args.State;
        UnityEngine.WSA.Application.InvokeOnAppThread(() =>
        {
            if (item.Source.Handedness == SpatialInteractionSourceHandedness.Right)
            {
                rightChildHand.localScale = Vector3.one * 0.5f;
            }
            else if (item.Source.Handedness == SpatialInteractionSourceHandedness.Left)
            {
                leftChildHand.localScale = Vector3.one * 0.5f;
            }
        }, false);
    }
#endif
}
