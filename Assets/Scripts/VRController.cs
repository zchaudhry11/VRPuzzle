using UnityEngine;
using System.Collections;
using Leap;

public class VRController : MonoBehaviour 
{
    public float speed = 5.0f;
    public float maxVelocityChange = 8.0f;
    public float maxJumpHeight = 3.0f;
    public float velocityDamping = 5.0f;

    public bool grounded = false;

    public float gravity = 9.0f;

    public GameObject bluePortal;
    public GameObject orangePortal;

    private Vector3 MoveThrottle = Vector3.zero;

    //Player flags
    public bool isJumping = false;
    public bool isMoving = false;
    public bool isPickingUp = false;

    //Item Pickup
    Transform entity; //original transform
    GameObject item;

    //Leapmotion integration
    Controller leap_controller_;
    Frame frame;
    HandList hands;
    Hand firstHand;

    void Awake()
    {
        this.GetComponent<Rigidbody>().freezeRotation = true;
        this.GetComponent<Rigidbody>().useGravity = false;

        leap_controller_ = new Controller();
    }

    void Update()
    {
        //Leapmotion integration
        if (leap_controller_ != null)
        {
            frame = leap_controller_.Frame();
        }

        if (frame != null)
        {
            hands = frame.Hands;
        }

        if (hands != null)
        {
            firstHand = hands[0];
        }
        if (firstHand != null)
        {
            if (hands != null)
            {
                if (firstHand.IsLeft == true && hands.Count == 1)
                {
                    FirePortal(bluePortal);
                }
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            FirePortal(bluePortal);
        }

        if (firstHand != null)
        {
            if (hands != null)
            {
                if (firstHand.IsRight == true && hands.Count == 1)
                {
                    FirePortal(orangePortal);
                }
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            FirePortal(orangePortal);
        }
        PickUp();

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Camera.main.transform.localEulerAngles.y, transform.localEulerAngles.z);
        
        transform.forward = new Vector3(transform.forward.x, Camera.main.transform.forward.y, transform.forward.z);
        //Debug.DrawLine(transform.position, transform.forward, Color.red, 100);
    }

    void FixedUpdate()
    {
        if (grounded == true)
        {
            PlayerMovement();
            if (isJumping == false)
            {
                PlayerJump();
            }
        }

        Vector3 gravityPower = new Vector3(0, -gravity * this.GetComponent<Rigidbody>().mass, 0);
        this.GetComponent<Rigidbody>().AddForce(gravityPower, ForceMode.Force);
    }

    void PlayerMovement()
    {
        Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;
        
        if (targetVelocity.x != 0 || targetVelocity.z != 0)
        {
            isMoving = true;
        }

        bool moveForward = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        bool moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        bool moveBack = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);

        Quaternion ort = transform.rotation;
        Vector3 ortEuler = ort.eulerAngles;
        ortEuler.z = ortEuler.x = 0f;
        ort = Quaternion.Euler(ortEuler);

        Vector3 velocityChange = targetVelocity - this.GetComponent<Rigidbody>().velocity;
        if (moveForward == true)
        {
            velocityChange = ort * transform.forward;
            velocityChange /= velocityDamping;
        }
        if (moveBack == true)
        {
            velocityChange = ort * -transform.forward;
            velocityChange /= velocityDamping;
        }
        if (moveLeft == true)
        {
            velocityChange = ort * transform.forward;
            velocityChange /= velocityDamping;
            float temp = -velocityChange.z;
            velocityChange.z = velocityChange.x;
            velocityChange.x = temp;
        }
        if (moveRight == true)
        {
            velocityChange = ort * transform.forward;
            velocityChange /= velocityDamping;
            float temp = velocityChange.z;
            velocityChange.z = -velocityChange.x;
            velocityChange.x = temp;
        }

        if (moveForward && moveRight)
        {
            velocityChange = ort * transform.forward;
            velocityChange /= velocityDamping * 2;
            float temp = velocityChange.z;
            velocityChange.z = -velocityChange.x;
            velocityChange.x = temp;

        }

        if (moveForward && moveLeft)
        {
            velocityChange = ort * transform.forward;
            velocityChange /= velocityDamping*2;
            velocityChange.x = -velocityChange.z / 2;
        }
        if (moveBack && moveRight)
        {
            velocityChange = ort * -transform.forward;
            velocityChange /= velocityDamping*2;
            velocityChange.x = velocityChange.z / 2;
        }
        if (moveBack && moveLeft)
        {
            velocityChange = ort * -transform.forward;
            velocityChange /= velocityDamping*2;
            velocityChange.x = -velocityChange.z / 2;
        }

        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        this.GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Vector3 jumpHeight = new Vector3(0, CalculateJumpHeight(), 0);
            this.GetComponent<Rigidbody>().AddForce(jumpHeight, ForceMode.VelocityChange);
            isJumping = true;
            grounded = false;
        }
    }

    void FirePortal(GameObject portal)
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 500))
        {
            GameObject instanceBlue = GameObject.FindGameObjectWithTag("BluePortal");
            GameObject instanceOrange = GameObject.FindGameObjectWithTag("OrangePortal");
            Quaternion surfaceRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            /*if (portal == bluePortal)
            {
                surfaceRotation = Quaternion.Euler(new Vector3(surfaceRotation.eulerAngles.x + 270, surfaceRotation.eulerAngles.y+180, surfaceRotation.eulerAngles.z));
            }
            else
            {
                surfaceRotation = Quaternion.Euler(new Vector3(surfaceRotation.eulerAngles.x, surfaceRotation.eulerAngles.y+90, surfaceRotation.eulerAngles.z + 90));
            }*/

        if (hit.transform.gameObject.tag == "PortalSurface")
        {
            GameObject clone = Instantiate(portal, hit.point, surfaceRotation) as GameObject;
            
            if (instanceBlue != null && portal.tag == "BluePortal")
            {
                Destroy(instanceBlue);
                
            }
            if (instanceOrange != null && portal.tag == "OrangePortal")
            {
                Destroy(instanceOrange);
            }
        }
            //Debug.DrawLine(transform.position, hit.point, Color.green, 100);
        }
    }

    void PickUp()
    {
        if (Input.GetButtonDown("Action") && isPickingUp == false)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                if (hit.transform.gameObject.tag == "PuzzleEntity")
                {
                    entity = hit.transform.parent;
                    item = hit.transform.gameObject;
                    Transform playerCam = this.transform;

                    //hit.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
                    hit.transform.SetParent(playerCam); //parent object to player camera
                    hit.transform.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    hit.transform.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    isPickingUp = true;
                }
            }
        }
        else if (Input.GetButtonDown("Action") && isPickingUp == true) //drop item
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().freezeRotation = false;
            item.transform.SetParent(entity);
            isPickingUp = false;
        }
    }

    float CalculateJumpHeight()
    {
        return Mathf.Sqrt(maxJumpHeight);
    }

    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnCollisionEnter()
    {
        isJumping = false;
    }

}
