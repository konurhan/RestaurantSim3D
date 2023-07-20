using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableObject : MonoBehaviour
{
    public bool isPlaced;//true for already placed tables, false for newly created tables
    public Vector3 lastPosition;
    private BoxCollider boxcollider;

    private void Awake()
    {
        boxcollider = gameObject.GetComponent<BoxCollider>();
    }
    private void Start()
    {
        lastPosition = gameObject.transform.position;
    }

    public void Place()
    {
        if(CanBePlaced())
        {
            isPlaced = true;
            lastPosition = gameObject.transform.position;
            //gameObject.transform.SetParent(RestaurantManager.Instance.RestaurantComponents.GetChild(1));//adding table to tables in hierarchy
            //RestaurantManager.Instance.CacheSeats();
        }
        else
        {
            //isPlaced = false;
            gameObject.transform.position = lastPosition;//if cannot be placed return it to original position
        }
    }

    public bool CanBePlaced()//if it collides with any other collider
    {
        LayerMask layer = LayerMask.GetMask("WallsTables");
        //Debug.Log("placableobj layer is: " + gameObject.layer);
        Vector3 size = new Vector3(boxcollider.size.x * gameObject.transform.localScale.x, boxcollider.size.y * gameObject.transform.localScale.y, boxcollider.size.z * gameObject.transform.localScale.z);
        Collider[] collided = Physics.OverlapBox(gameObject.transform.position + boxcollider.center, size/2, Quaternion.identity, layer);
        if (collided.Length>1)//always collides with itself
        {
            Debug.Log("placeableobject is colliding with other obejct/s");
            foreach(Collider col in collided)
            {
                Debug.Log("collided obj name is: " +col.gameObject.name);
            }
            return false;
        }
        else
        {
            Debug.Log("placeableobject is not colliding with any other obejct");
            return true;
        }
    }
    
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 size = new Vector3(boxcollider.size.x * gameObject.transform.localScale.x, boxcollider.size.y * gameObject.transform.localScale.y, boxcollider.size.z * gameObject.transform.localScale.z);
        Gizmos.DrawCube(gameObject.transform.position + boxcollider.center, size);
    }*/
}
