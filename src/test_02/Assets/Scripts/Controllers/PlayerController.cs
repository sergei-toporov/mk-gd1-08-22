using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/**
 * The "Character" class replaced with the "PlayerController" class.
 */
public class PlayerController : MonoBehaviour
{
    public float gravity = -9.81f;
    public float speed = 10.0f;
    public Transform transformCamera;
    public Transform pivot;
    public GameObject bullet;
    public Transform[] Guns;

    private float ySpeed;
    private bool isJump;
    private bool shot = false;
    private bool isSwitchingGun = false;

    private int _currentGun = 0;
    public int newGun = -1;

    /**
     * Character animator is not in Update() anymore.
     */
    protected Animator characterAnimator;
    public Animator CharacterAnimator { get => characterAnimator ?? GetComponent<Animator>(); }

    protected CharacterController charController;
    public CharacterController CharController { get => charController ?? GetComponent<CharacterController>(); }

    void Start() { }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float rotation = Input.GetAxis("Mouse X");


        Vector3 inputDirection = new Vector3(horizontal, 0.0f, vertical);
        Vector3 movement = new Vector3(horizontal * speed, gravity, vertical * speed);

        var inputAngle = horizontal < 0.0f ? -Vector3.Angle(Vector3.forward, inputDirection) : Vector3.Angle(Vector3.forward, inputDirection);

        CharacterAnimator.SetFloat("direction", inputAngle / 180.0f);
        CharacterAnimator.SetFloat("idle", inputDirection.magnitude);
        movement = Quaternion.AngleAxis(transformCamera.rotation.eulerAngles.y, Vector3.up) * movement;




        CharController.Move(movement * Time.deltaTime);
        CharController.transform.Rotate(Vector3.up, rotation);
        if (Input.GetMouseButtonDown(0) && shot == false && !isSwitchingGun)
        {
            StartCoroutine(StartShotAnimation());
        }
    }

    IEnumerator StartShotAnimation()
    {
        shot = true;
        CharacterAnimator.SetTrigger("shot");
        yield return new WaitForSeconds(0.1f);
        SpawnProjectile();
        yield return new WaitForSeconds(0.75f);
        shot = false;
    }

    IEnumerator SwitchGun(int gunIndex)
    {
        isSwitchingGun = true;
        CharacterAnimator.SetTrigger("SwitchGun");
        yield return new WaitForSeconds(0.9f);
        Guns[_currentGun].gameObject.SetActive(false);
        _currentGun = gunIndex;
        Guns[_currentGun].gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        isSwitchingGun = false;
    }

    void SpawnProjectile()
    {
        var bulletInstance = Instantiate(bullet, pivot.position, pivot.rotation);
        //Debug.Break();
        bulletInstance.SetActive(true);
        var gun = Guns[_currentGun].GetComponent<Gun>();
        bulletInstance.transform.Rotate(Vector3.left, gun.Angle);
        bulletInstance.GetComponent<Rigidbody>().AddForce(bulletInstance.transform.forward * gun.Force);

    }

    private void FixedUpdate() { }

    private void LateUpdate() { }

    /**
     * Added a method to call by weapon switchers instead of Update() checking.
     */
    public void SwitchActiveGun(AmmoBase providedAmmo)
    {
        if (providedAmmo.Gun != -1 && _currentGun != providedAmmo.Gun && !isSwitchingGun)
        {
            StartCoroutine(SwitchGun(providedAmmo.Gun));
        }
    }
}
