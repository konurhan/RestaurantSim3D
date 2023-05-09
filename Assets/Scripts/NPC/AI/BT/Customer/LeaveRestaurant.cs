using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRestaurant : Node
{
    Customer customer;
    public LeaveRestaurant(Customer customer)
    {
        this.customer = customer;
    }
    public override NodeState Evaluate()
    {
        customer.seatTransform.gameObject.GetComponent<SeatController>().LeaveTheSeat();//test if this operation is succesful
        customer.seatTransform = null;
        throw new System.NotImplementedException();
    }
}
