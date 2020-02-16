using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//class manage the selective marker on an object 
public class SelectMarker : MonoBehaviour
{
    private static SelectMarker instance;

  



    [SerializeField]
    private GameObject marker;



    public GameObject Mymarker { get { return marker; } }

    void Start()
    {
   

        marker.SetActive(false);
    }



    public void Activate()
    {
        marker.SetActive(true);
    }

    public void Deactivate()
    {
        marker.SetActive(false);

    }


}
