using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = transform.position - PlacementSystem.Instance.GetMouseWorldPosition();
        PlacementSystem.Instance.objectToPlace = gameObject.GetComponent<PlacableObject>();
        MenuController.Instance.VCamTarget.gameObject.GetComponent<VCamController>().rotationEnabled = false;

    }

    private void OnMouseDrag()
    {
        Vector3 pos = offset + PlacementSystem.Instance.GetMouseWorldPosition();
        transform.position = PlacementSystem.Instance.SnapPositionToGrid(pos);
    }

    private void OnMouseUp()
    {
        PlacementSystem.Instance.PlaceObject();
        MenuController.Instance.VCamTarget.gameObject.GetComponent<VCamController>().rotationEnabled = true;
    }

}

