using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerScript : MonoBehaviour
{
    public bool doorisopen = false;
    public bool animateable = true;



    public void cananimate()
    {
        animateable = true;
    }

    public void open()
    {
        doorisopen = true;
    }
    public void close()
    {
        doorisopen = false;
    }
}
