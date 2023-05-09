using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatController : MonoBehaviour
{
    [SerializeField] private GameObject occupant;
    void Start()
    {
        occupant = null;
    }

    public void SetOccupant(GameObject occupant)
    {
        this.occupant = occupant;
    }

    public void LeaveTheSeat()
    {
        occupant = null;
    }

    public GameObject GetOccupant()
    {
        return occupant;
    }
}
