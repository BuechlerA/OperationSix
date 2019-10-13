using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Entity
{
    public LayerMask targetLayer;
    public LayerMask obstLayer;
    public List<Transform> enemiesInArea = new List<Transform>();

    public CharacterController characterController;
    public ReticleBehaviour reticle;

    public Transform target;

    [SerializeField]
    private Camera cam;

    private float currentSpeed;
    public float walkSpeed = 2f;
    public float runSpeed = 4f;

    public float gravity = 30f;
    private float verticalSpeed = 0f;
    private float slopeForce = 100f;
    private float slopeForceRayLength = 5f;

    public bool isMoving = false;
    public bool isRunning = false;
    public bool isInteracting = false;
    public bool isShooting = false;

    public bool hasTargetLocked = false;
    public bool isTargetVisible = false;

    public void GetInput(Vector3 moveVector, Vector3 viewVector)
    {
        PlayerMove(moveVector);
        PlayerView(viewVector);
    }

    public void PlayerMove(Vector3 moveVector)
    {
        if (isRunning)
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }
        characterController.Move(transform.TransformDirection(moveVector.x * 0.5f, verticalSpeed, moveVector.z) * currentSpeed * Time.deltaTime);
    }

    public void PlayerView(Vector3 viewVector)
    {
        transform.localEulerAngles += new Vector3(0, viewVector.y, 0);
        cam.transform.localEulerAngles += new Vector3(Mathf.Clamp(-viewVector.x, -80f, 80f), 0, 0);
    }

    public void PlayerInteract()
    {

    }

    public void PlayerShootGun()
    {
        GetComponentInChildren<GunBase>().ShootGun();     
    }

    public void PlayerReloadGun()
    {
        GetComponentInChildren<GunBase>().ReloadGun();
    }

    private void Update()
    {
        CheckGrounded();
        AimAssist();
    }

    private void CheckGrounded()
    {
        if (characterController.isGrounded)
        {
            verticalSpeed = 0f;
        }
        if (!characterController.isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        else if (isOnSlope())
        {
            verticalSpeed -= gravity * slopeForce * Time.deltaTime;
        }
    }

    private void AimAssist()
    {
        enemiesInArea.Clear();

        if (enemiesInArea.Count <= 0)
        {
            target = null;
        }

        Collider[] enemiesList = Physics.OverlapSphere(transform.position, 500f, targetLayer);

        for (int i = 0; i < enemiesList.Length; i++)
        {
            enemiesInArea.Add(enemiesList[i].transform);
        }

        foreach (Transform headmarker in enemiesInArea)
        {
            Ray ray = new Ray(cam.transform.position, headmarker.position - cam.transform.position);
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, headmarker.position - cam.transform.position, Color.red);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer == 10)
                {
                    isTargetVisible = true;
                    Debug.Log(GetClosestEnemy(enemiesInArea));
                    target = headmarker;
                }
                else
                {
                    isTargetVisible = false;
                    target = null;
                }
            }
        }

        //Set Reticle to Target position
        if (target != null)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(target.position);
            Rect viewRect = new Rect((Screen.width * 0.5f) - 75f, (Screen.height * 0.5f) - 75f, 150f, 150f);
        
            if (isTargetVisible)
            {
                if (viewRect.Contains(screenPos) && screenPos.z > 0)
                {
                    reticle.transform.position = screenPos;
                    GetComponentInChildren<GunBase>().transform.LookAt(target.position);
                    hasTargetLocked = true;
                }
                else
                {          
                    reticle.transform.position = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
                    GetComponentInChildren<GunBase>().transform.localEulerAngles = Vector3.zero;
                    hasTargetLocked = false;
                }
            }
        }
    }

    private bool isOnSlope()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, characterController.height / 2f * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                return true;
            }
        }  
        return false;
    }

    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
