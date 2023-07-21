using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VCamController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 35f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private CinemachineVirtualCamera vcam;
    public bool edgescrollingEnabled = true;
    public bool mouseDragEnabled = true;
    public bool rotationEnabled = true;

    [SerializeField] private bool mouseDragging = true;
    [SerializeField] private Vector2 lastMousePosition;

    private Vector3 followOffset;
    private Vector3 zoomDir;

    private float minOffset = 5f;
    private float maxOffset = 15f;

    private void Start()
    {
        followOffset = vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        zoomDir = vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.normalized;
    }
    void Update()
    {
        CameraMovementControl();
        CameraRotationControl();
        CameraZoomControl();
        BoundMovementArea();
    }

    public void BoundMovementArea()//don't let the camera to go too far
    {
        if (transform.position.x < -25) transform.position = new Vector3(-25, transform.position.y, transform.position.z);
        if (transform.position.x > 55) transform.position = new Vector3(55, transform.position.y, transform.position.z);
        if (transform.position.y < 5) transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        if (transform.position.y > 30) transform.position = new Vector3(transform.position.x, 30, transform.position.z);
        if (transform.position.z < -25) transform.position = new Vector3(transform.position.x, transform.position.y, -25);
        if (transform.position.z > 55) transform.position = new Vector3(transform.position.x, transform.position.y, 55);
    }

    public void CameraMovementControl()
    {
        Vector3 inputDirection = Vector3.zero;

        #region movementWASD
        if (Input.GetKey(KeyCode.W)) inputDirection.z += 1f;
        if (Input.GetKey(KeyCode.S)) inputDirection.z -= 1f;
        if (Input.GetKey(KeyCode.D)) inputDirection.x += 1f;
        if (Input.GetKey(KeyCode.A)) inputDirection.x -= 1f;
        #endregion

        #region movementEdgeScrolling
        if (edgescrollingEnabled)
        {
            float horizontalPadding = Screen.width / 10;
            float verticalPadding = Screen.height / 10;
            if (Input.mousePosition.x < horizontalPadding) inputDirection.x -= 1f;
            if (Input.mousePosition.x > Screen.width - horizontalPadding) inputDirection.x += 1f;
            if (Input.mousePosition.y < verticalPadding) inputDirection.z -= 1f;
            if (Input.mousePosition.y > Screen.height - verticalPadding) inputDirection.z += 1f;
        }
        #endregion

        #region movementMouseDrag
        if (mouseDragEnabled)
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseDragging = true;
                lastMousePosition = Input.mousePosition;//to avoid jumps caused by value of lastMousePos from previous drag
            }
            if (Input.GetMouseButtonUp(1)) mouseDragging = false;

            if (Input.GetMouseButton(1))
            {
                if (mouseDragging)
                {
                    Vector2 drag = -(new Vector2(Input.mousePosition.x, Input.mousePosition.y) - lastMousePosition) / 4;
                    lastMousePosition = Input.mousePosition;
                    inputDirection.x += drag.x;
                    inputDirection.z += drag.y;
                }
            }
        }
        #endregion

        Vector3 moveDirection = transform.forward * inputDirection.z + transform.right * inputDirection.x;
        transform.position += movementSpeed * Time.deltaTime * moveDirection;
    }

    public void CameraRotationControl()
    {
        if (!rotationEnabled) return;
        #region rotationQE
        Vector3 rotationAngles = Vector3.zero;
        if (Input.GetKey(KeyCode.Q)) rotationAngles.y += 1f;
        if (Input.GetKey(KeyCode.E)) rotationAngles.y -= 1f;
        transform.eulerAngles += rotationSpeed * rotationAngles * Time.deltaTime;
        #endregion
    }

    public void CameraZoomControl()
    {
        //float ratio = Mathf.Abs(vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y / vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z);
        
        //Vector3 zoomDir = vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.normalized;
        //int zoomAmount = 0;
        if(Input.mouseScrollDelta.y > 0)
        {
            followOffset -= zoomDir;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset += zoomDir;
        }
        //clamp follow offset here
        if(followOffset.magnitude < minOffset)
        {
            followOffset = zoomDir * minOffset;
        }
        if(followOffset.magnitude > maxOffset)
        {
            followOffset = zoomDir * maxOffset;
        }
        vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(vcam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * 10);
        
    }
}
