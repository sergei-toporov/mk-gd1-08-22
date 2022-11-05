using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Buttons controller.
 */
public class ButtonController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    /**
     * Camera pitch direction flag.
     *
     * @param int
     */
    [SerializeField][Range(CameraController.RotateRightOrUp, CameraController.RotateLeftOrDown)] private int cameraPitchDirection;
    
    /**
     * Button component.
     * 
     * @param Button
     */
    private Button button;
    public Button Button { get => button = button != null ? button : GetComponent<Button>(); }

    /**
     * Button state flag: being hold or not.
     *
     * @param bool
     */
    private bool isHeld = false;

    /**
     * Camera controller instance.
     * 
     * @param CameraController
     */
    private CameraController cameraController;
    public CameraController CameraController {
        get
        {
            return cameraController = cameraController != null
            ? cameraController
            : GameManager.Manager.PlayerController.PlayerCamera.GetComponent<CameraController>();
        }
    }

    /**
     * {@inheritdoc}
     */
    void Start()
    {
        if (cameraPitchDirection == 0)
        {
            Debug.LogError("Camera Pitch Direction shoul be equal either -1 or 1!");
            Application.Quit();
        }
    }

    /**
     * {@inheritdoc}
     */
    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    /**
     * {@inheritdoc}
     */
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    /**
     * {@inheritdoc}
     */
    void Update()
    {
        if (isHeld)
        {
            CameraController.PitchCamera(cameraPitchDirection);
        }
    }
}
