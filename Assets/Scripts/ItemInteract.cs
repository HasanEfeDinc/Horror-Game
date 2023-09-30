using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private LayerMask PickupLayer;
    [SerializeField] private LayerMask DoorLayer;
    [SerializeField] private LayerMask DoubleDoorLayer;
    [SerializeField] private LayerMask LockerLayer;
    [SerializeField] private LayerMask GhostIdleLayer;
    [SerializeField] private GameObject Flashlight, Emf, Cross, Box, Book, Thermometer;
    [SerializeField] private Transform SpawnTransform;
    [SerializeField] private GameObject ThrowFlashlight, ThrowEmf, ThrowCross, ThrowBox, ThrowBook, ThrowThermometer;
    [SerializeField] private Image Image1, Image2, Image3;
    [SerializeField] Sprite FlashlightSprite, EmfSprite, CrossSprite, BoxSprite, BookSprite, ThermometerSprite;
    [SerializeField] private GameObject Light;
    [SerializeField] GameObject TakeableFlashlight,TakeableEmf,TakeableCross,TakeableBox,TakeableBook,TakeableThermometer;
    [SerializeField] private GameObject EmfTurnon,BoxTurnon;
    [SerializeField] private GameObject EmfDefault;
    
    
    public GameObject InteractImage;
    public GameObject OpenImage;
    public GameObject ThermometerPanel, DefaultThermometerPanel;
    public GameObject BoxPanel;
    public GameObject ThreatMessage;
    
    

    [SerializeField] private AudioSource BoxSound;
    

    private int PressFCount = 0;
    private int emfactivatecount = 0;
    private int boxactivatecount = 1;
    private int Thermometeractivatecount = 1;
    private int IventoryCount;
    

    private float distance = 2;
    private float distance2 = 10;
    
    private String[] Items = new string[3];
    private Image[] Images = new Image[3];

    private bool WieldingFlashlight = false, WieldingEmf= false, WieldingCross= false, WieldingBox= false, WieldingBook= false, WieldingThermometer= false;
    private bool Handisempty = true;
    private bool Item1filled, Item2filled, Item3filled;
    private bool Pressed1, Pressed2, Pressed3;
    private bool flashlighttaken = false;
    private bool threated = false;
    
    
    
    
    
    private RaycastHit hit;

    private void Start()
    {
        
        Images[0] = Image1;
        Images[1] = Image2;
        Images[2] = Image3;
    }

    private void Update()
    {
        if (IventoryCount > 3)
        {
            IventoryCount = 3;
        }

        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, distance2, GhostIdleLayer))
        {
            Debug.Log(Game.Instance.GhostIdleSeen);
            Game.Instance.GhostIdleSeen = true;
        }
        
        if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, distance, PickupLayer))
        {
            
            InteractImage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (hit.transform.gameObject.name)
                {
                    case "Flashlight":
                        flashlighttaken = true;
                        IventoryCount++;
                        
                        if (IventoryCount <= 3)
                        {
                            Flashlight.SetActive(true);
                            addintoarray("Flashlight");
                            InventoryPhoto();
                            TakeableFlashlight.SetActive(false);
                            
                        }

                        break;
                    case "Cross":
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            addintoarray("Cross");
                            InventoryPhoto();
                            TakeableCross.SetActive(false);
                            
                        }
                        break;
                    case "Thermometer":
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            addintoarray("Thermometer");
                            InventoryPhoto();
                            TakeableThermometer.SetActive(false);
                            
                        }
                        
                        break;
                    case "Closed book" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            addintoarray("Closed book");
                            InventoryPhoto();
                            TakeableBook.SetActive(false);
                            
                        }
                        break;
                    case "Object_4":
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            addintoarray("Object_4");
                            InventoryPhoto();
                            TakeableBox.SetActive(false);
                            
                        }
                        break;
                    case "Plane.001":
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            addintoarray("Plane.001");
                            InventoryPhoto();
                            TakeableEmf.SetActive(false);
                            
                        }
                        break;
                    // Pick Up On Ground
                    case "BookGround(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("BookGround(Clone)"));
                            addintoarray("Closed book");
                            InventoryPhoto();
                        }
                        break;
                    case "EMFGround(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("EMFGround(Clone)"));
                            addintoarray("Plane.001");
                            InventoryPhoto();
                        }
                        break;
                    case "BoxGround(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("BoxGround(Clone)"));
                            addintoarray("Object_4");
                            InventoryPhoto();
                        }
                        break;
                    case "CrossGround(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("CrossGround(Clone)"));
                            addintoarray("Cross");
                            InventoryPhoto();
                            
                        }
                        break;
                    case "FlashlightGround(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("FlashlightGround(Clone)"));
                            Flashlight.SetActive(true);
                            addintoarray("Flashlight");
                            InventoryPhoto();
                        }
                        break;
                    case "Thermometer(Clone)" :
                        IventoryCount++;
                        if (IventoryCount <= 3)
                        {
                            Destroy(GameObject.Find("Thermometer(Clone)"));
                            addintoarray("Thermometer");
                            InventoryPhoto();
                        }
                        break;
                        

                }
            }
        }
        else
        {
            InteractImage.SetActive(false);
        }

         if(Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, distance, DoorLayer))
        {
            OpenImage.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (hit.transform.gameObject.GetComponent<DoorScript>().doorisopen == false && hit.transform.gameObject.GetComponent<DoorScript>().animateable == true)
                {
                    hit.transform.gameObject.GetComponent<DoorScript>().animateable = false;
                    hit.transform.gameObject.GetComponent<Animator>().Play("opening");

                }
                if (hit.transform.gameObject.GetComponent<DoorScript>().doorisopen == true && hit.transform.gameObject.GetComponent<DoorScript>().animateable == true)
                {
                    hit.transform.gameObject.GetComponent<DoorScript>().animateable = false;
                    hit.transform.gameObject.GetComponent<Animator>().Play("closing");

                }
            }
        }
         else
         {
             OpenImage.SetActive(false);
         }
         if(Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, distance, DoubleDoorLayer))
         {
             OpenImage.SetActive(true);
             if (Input.GetKeyDown(KeyCode.E))
             {
                 if (hit.transform.gameObject.GetComponentInParent<DoorScript>().doorisopen == false && hit.transform.gameObject.GetComponentInParent<DoorScript>().animateable == true)
                 {
                     
                     hit.transform.gameObject.GetComponentInParent<DoorScript>().animateable = false;
                     hit.transform.gameObject.GetComponentInParent<Animator>().Play("DD Opening");

                 }
                 if (hit.transform.gameObject.GetComponentInParent<DoorScript>().doorisopen == true && hit.transform.gameObject.GetComponentInParent<DoorScript>().animateable == true)
                 {
                     hit.transform.gameObject.GetComponentInParent<DoorScript>().animateable = false;
                     hit.transform.gameObject.GetComponentInParent<Animator>().Play("DD Closing");

                 }
             }
         }

         if (Physics.Raycast(CameraTransform.position, CameraTransform.forward, out hit, distance, LockerLayer))
         {
             OpenImage.SetActive(true);
             if (Input.GetKeyDown(KeyCode.E))
             {
                 if (Input.GetKeyDown(KeyCode.E))
                 {
                     if (hit.transform.gameObject.GetComponent<LockerScript>().doorisopen == false && hit.transform.gameObject.GetComponent<LockerScript>().animateable == true)
                     {
                         hit.transform.gameObject.GetComponent<LockerScript>().animateable = false;
                         hit.transform.gameObject.GetComponent<Animator>().Play("lockeropening");

                     }
                     if (hit.transform.gameObject.GetComponent<LockerScript>().doorisopen == true && hit.transform.gameObject.GetComponent<LockerScript>().animateable == true)
                     {
                         hit.transform.gameObject.GetComponent<LockerScript>().animateable = false;
                         hit.transform.gameObject.GetComponent<Animator>().Play("lockerclosing");

                     }
                 }
             }
         }
         
         
         
         if (Input.GetKeyDown(KeyCode.F) && flashlighttaken == true)
         {
             PressFCount++;
             if (PressFCount % 2 == 0)
             {
                 Light.SetActive(true);
             }
             if (PressFCount % 2 != 0)
             {
                 Light.SetActive(false);
             }
         }

         
         ChangeHands();
         ThrowItem();
         ActivateItem();
         
        




    }

    void addintoarray(String ıtemname)
    {
        for (int b = 0; b < Items.Length; b++)
        {
            if (Items[b] == null)
            {
                Items[b] = ıtemname;
                break;
            }
            
        }
        


    }

    void InventoryPhoto()
    {
        for (int a = 0; a < Items.Length; a++)
        {
            if (Items[a] == "Flashlight")
            {
                
                Images[a].sprite = FlashlightSprite;
                Images[a].gameObject.SetActive(true);
                
                
            }
            if (Items[a] == ("Cross"))
            {
                Images[a].sprite = CrossSprite;
                Images[a].gameObject.SetActive(true);
                
            }
            if (Items[a] == ("Thermometer"))
            {
                Images[a].sprite = ThermometerSprite;
                Images[a].gameObject.SetActive(true);
                
            }
            else if (Items[a] == ("Closed book"))
            {
                Images[a].sprite = BookSprite;
                Images[a].gameObject.SetActive(true);
               
            }
            else if (Items[a] == ("Object_4"))
            {
                Images[a].sprite = BoxSprite;
                Images[a].gameObject.SetActive(true);
                
            }
            else if (Items[a] == ("Plane.001"))
            {
                Images[a].sprite = EmfSprite;
                Images[a].gameObject.SetActive(true);
                
            }
        }
        
        
    }

    void ChangeHands()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Pressed1 = true;
            Pressed2 = false;
            Pressed3 = false;
            if (Handisempty == false)
            {
                WieldingBook= false;
                WieldingBox= false;
                WieldingCross= false;
                WieldingEmf= false;
                WieldingFlashlight= false;
                WieldingThermometer = false;
                Cross.SetActive(false);
                ThermometerPanel.SetActive(false);
                DefaultThermometerPanel.SetActive(false);
                BoxPanel.SetActive(false);
                ThreatMessage.SetActive(false);
                Thermometer.SetActive(false);
                Book.SetActive(false);
                Box.SetActive(false);
                BoxSound.Stop();
                Emf.SetActive(false);
                Handisempty = true;
            }
            if (Items[0] == "Flashlight" )
            { 
                WieldingFlashlight = true;
                Handisempty = false;
                

            }
            if (Items[0] == ("Cross"))
            {
                WieldingCross = true;
                Cross.SetActive(true);
                Handisempty = false;
            }
            if (Items[0] == ("Thermometer"))
            {
                WieldingThermometer = true;
                Thermometer.SetActive(true);
                ThermometerPanel.SetActive(true);
                Handisempty = false;
            }
            else if (Items[0] == ("Closed book"))
            {
                WieldingBook = true;
                Book.SetActive(true);
                if (threated)
                {
                    ThreatMessage.SetActive(true);
                }
                Handisempty = false;
            }
            else if (Items[0] == ("Object_4"))
            {
                WieldingBox = true;
                Box.SetActive(true);
                BoxPanel.SetActive(true);
                BoxSound.Play();
                Handisempty = false;
            }
            else if (Items[0] == ("Plane.001"))
            {
                WieldingEmf = true;
                Emf.SetActive(true);
                Handisempty = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Pressed1 = false;
            Pressed2 = true;
            Pressed3 = false;
            if (Handisempty == false)
            {
                WieldingBook= false;
                WieldingBox= false;
                WieldingCross= false;
                WieldingEmf= false;
                WieldingFlashlight= false;
                WieldingThermometer = false;
                Cross.SetActive(false);
                ThermometerPanel.SetActive(false);
                DefaultThermometerPanel.SetActive(false);
                BoxPanel.SetActive(false);
                ThreatMessage.SetActive(false);
                Thermometer.SetActive(false);
                Book.SetActive(false);
                Box.SetActive(false);
                BoxSound.Stop();
                Emf.SetActive(false);
                Handisempty = true;
            }
            if (Items[1] == "Flashlight")
            { 
                WieldingFlashlight = true;
                Handisempty = false;
                
            }
            if (Items[1] == ("Cross"))
            { 
                WieldingCross = true;
                Cross.SetActive(true);
                Handisempty = false;
            }
            if (Items[1] == ("Thermometer"))
            { 
                WieldingThermometer = true;
                Thermometer.SetActive(true);
                ThermometerPanel.SetActive(true);
                Handisempty = false;
            }
            else if (Items[1] == ("Closed book"))
            { 
                WieldingBook = true;
                Book.SetActive(true);
                if (threated)
                {
                    ThreatMessage.SetActive(true);
                }
                Handisempty = false;
            }
            else if (Items[1] == ("Object_4"))
            { 
                WieldingBox = true;
                Box.SetActive(true);
                BoxPanel.SetActive(true);
                BoxSound.Play();
                Handisempty = false;
            }
            else if (Items[1] == ("Plane.001"))
            { 
                WieldingEmf = true;
                Emf.SetActive(true);
                Handisempty = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Pressed1 = false;
            Pressed2 = false;
            Pressed3 = true;
            if (Handisempty == false)
            {
                WieldingBook= false;
                WieldingBox= false;
                WieldingCross= false;
                WieldingEmf= false;
                WieldingFlashlight= false;
                WieldingThermometer = false;
                Cross.SetActive(false);
                ThermometerPanel.SetActive(false);
                DefaultThermometerPanel.SetActive(false);
                BoxPanel.SetActive(false);
                ThreatMessage.SetActive(false);
                Thermometer.SetActive(false);
                Book.SetActive(false);
                Box.SetActive(false);
                BoxSound.Stop();
                Emf.SetActive(false);
                Handisempty = true;
            }
            if (Items[2] == "Flashlight" )
            { 
                WieldingFlashlight = true;
                Handisempty = false;
                
            }
            if (Items[2] == ("Cross"))
            { 
                WieldingCross = true;
                Cross.SetActive(true);
                Handisempty = false;
            }
            if (Items[2] == ("Thermometer"))
            { 
                WieldingThermometer = true;
                Thermometer.SetActive(true);
                ThermometerPanel.SetActive(true);
                Handisempty = false;
            }
            else if (Items[2] == ("Closed book"))
            { 
                WieldingBook = true;
                Book.SetActive(true);
                if (threated)
                {
                    ThreatMessage.SetActive(true);
                }
                Handisempty = false;
            }
            else if (Items[2] == ("Object_4"))
            { 
                WieldingBox = true;
                Box.SetActive(true);
                BoxPanel.SetActive(true);
                BoxSound.Play();
                Handisempty = false;
            }
            else if (Items[2] == ("Plane.001"))
            { 
                WieldingEmf = true;
                Emf.SetActive(true);
                Handisempty = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            WieldingBook= false;
            WieldingBox= false;
            WieldingCross= false;
            WieldingEmf= false;
            WieldingFlashlight= false;
            WieldingThermometer = false;
            Cross.SetActive(false);
            ThermometerPanel.SetActive(false);
            DefaultThermometerPanel.SetActive(false);
            BoxPanel.SetActive(false);
            ThreatMessage.SetActive(false);
            Thermometer.SetActive(false);
            Book.SetActive(false);
            Box.SetActive(false);
            BoxSound.Stop();
            Emf.SetActive(false);
            Handisempty = true;
        }
    }

    void ThrowItem()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            if (WieldingBook == true)
            {
                ThreatMessage.SetActive(false);
                Book.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject BookOnGround = Instantiate(ThrowBook, spawnPosition, Quaternion.Euler(-90, SpawnTransform.rotation.eulerAngles.y, 0));
                BookOnGround.GetComponent<Rigidbody>().AddRelativeForce(0,0,20);
                WieldingBook = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;

                }
            }
            else if (WieldingBox == true)
            {
                BoxSound.Stop();
                BoxPanel.SetActive(false);
                Box.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject BoxOnGround = Instantiate(ThrowBox, spawnPosition, Quaternion.Euler(90, SpawnTransform.rotation.eulerAngles.y, 180));
                BoxOnGround.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 20);
                WieldingBox = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;

                }
            }
            else if (WieldingCross == true)
            {
                Cross.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject CrossOnGround = Instantiate(ThrowCross, spawnPosition, Quaternion.Euler(180, SpawnTransform.rotation.eulerAngles.y, 270));
                CrossOnGround.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 20);
                WieldingCross = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;

                }
                
            }
            else if (WieldingEmf == true)
            {
                Emf.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject EmfOnGround = Instantiate(ThrowEmf, spawnPosition, Quaternion.Euler(-90, SpawnTransform.rotation.eulerAngles.y, 0));
                EmfOnGround.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 20);
                WieldingEmf = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;
                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;
                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;
                }
                
            }
            else if (WieldingFlashlight == true)
            {
                Flashlight.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject FlashlightOnGround = Instantiate(ThrowFlashlight, spawnPosition, Quaternion.Euler(90, SpawnTransform.rotation.eulerAngles.y, 180));
                FlashlightOnGround.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 20);
                WieldingFlashlight = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;
                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;
                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;
                }
            }
            else if (WieldingThermometer == true)
            {
                ThermometerPanel.SetActive(false);
                DefaultThermometerPanel.SetActive(false);
                Thermometer.SetActive(false);
                Handisempty = true;
                Vector3 spawnPosition = SpawnTransform.position;
                GameObject ThermometerOnGround = Instantiate(ThrowThermometer, spawnPosition, Quaternion.Euler(180, SpawnTransform.rotation.eulerAngles.y, SpawnTransform.rotation.eulerAngles.z));
                ThermometerOnGround.GetComponent<Rigidbody>().AddRelativeForce(0, 0, 20);
                WieldingThermometer = false;
                if (Pressed1 == true)
                {
                    Images[0].gameObject.SetActive(false);
                    Images[0].sprite = null;
                    Items[0] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed2 == true)
                {
                    Images[1].gameObject.SetActive(false);
                    Images[1].sprite = null;
                    Items[1] = null;
                    IventoryCount = IventoryCount - 1;

                }
                if (Pressed3 == true)
                {
                    Images[2].gameObject.SetActive(false);
                    Images[2].sprite = null;
                    Items[2] = null;
                    IventoryCount = IventoryCount - 1;

                }
            }
        }
    }

    void ActivateItem()
    {
        
        if (Input.GetMouseButtonDown(1) && Handisempty == false)
        {
            if (WieldingEmf)
            {
                emfactivatecount++;
                if (emfactivatecount % 2 != 0)
                {
                    EmfTurnon.SetActive(true);
                    EmfDefault.SetActive(false);
                }
                if(emfactivatecount % 2 == 0)
                {
                    EmfTurnon.SetActive(false);
                    EmfDefault.SetActive(true);
                }
            }

            if (WieldingBox)
            {
                boxactivatecount++;
                if (boxactivatecount % 2 != 0)
                {
                    BoxTurnon.SetActive(true);
                    BoxSound.Play();
                    
                }
                if(boxactivatecount % 2 == 0)
                {
                    BoxTurnon.SetActive(false);
                    BoxSound.Stop();
                    
                }
                
            }
            if (WieldingThermometer)
            {
                Thermometeractivatecount++;
                if (Thermometeractivatecount % 2 != 0)
                {
                    ThermometerPanel.SetActive(true);
                    DefaultThermometerPanel.SetActive(false);
                    
                }
                if(Thermometeractivatecount % 2 == 0)
                {
                    ThermometerPanel.SetActive(false);
                    DefaultThermometerPanel.SetActive(true);
                    
                }
                
            }
        }
    }
    
    }
    
    

