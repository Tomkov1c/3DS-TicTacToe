using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.N3DS;
using UnityEngine.UI;

public class NavigationManager : MonoBehaviour
{
    [Header("Navigators")]
    public GameObject OnUpNaigationElement;
    public GameObject OnDownNaigationElement;
    public GameObject OnLeftNaigationElement;
    public GameObject OnRightNaigationElement;

    [Space]
    public Sprite ThisOnHover;

    [Header("Nav Actions")]
    public Component ActionCollection;

    [Space]
    public bool IsFirstHovered = false;
    public bool BackIsEnabled = true;

    public Sprite ThisNoHover;
    public Image ImageComponent;
    private bool IsInputListenerEnabled = false;

    private float horizontalInput;
    private float verticalInput;

    private bool navNext = true;
    private bool navButton = true;

    public bool IsHoverEnabled = true;
    public bool IsPressEnabled = true;

    private AudioManager audioManager;

    void Start()
    {
        ImageComponent = this.GetComponent<Image>();
        ThisNoHover = ImageComponent.sprite;

        if (IsFirstHovered)
        {
            ImageComponent.sprite = ThisOnHover;
        }

        if (IsFirstHovered)
        {
            IsInputListenerEnabled = true;
        }

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (IsInputListenerEnabled)
        {
            InputListenenr();
        }
    }

    public void Hover()
    {
        if (IsHoverEnabled)
        {
            audioManager.PlayOnHover();

            ImageComponent.sprite = ThisOnHover;
            this.IsInputListenerEnabled = true;
        }
    }

    public void Dehover()
    {
        ImageComponent.sprite = ThisNoHover;
        this.IsInputListenerEnabled = false;
    }

    void InputListenenr()
    {
        horizontalInput = GamePad.CirclePad.x == 0 ? Input.GetAxisRaw("Horizontal") : GamePad.CirclePad.x;
        verticalInput = GamePad.CirclePad.y == 0 ? Input.GetAxisRaw("Vertical") : GamePad.CirclePad.y;

        if (verticalInput > 0.5f && OnUpNaigationElement != null && navNext)
        {
            var temp = OnUpNaigationElement.GetComponent<NavigationManager>();
            temp.Hover();
            this.Dehover();
            navNext = false;
        }
        else if (verticalInput < -0.5f && OnDownNaigationElement != null && navNext)
        {
            var temp = OnDownNaigationElement.GetComponent<NavigationManager>();
            temp.Hover();
            this.Dehover();
            navNext = false;
        }
        else if (horizontalInput > 0.5f && OnRightNaigationElement != null && navNext)
        {
            var temp = OnRightNaigationElement.GetComponent<NavigationManager>();
            temp.Hover();
            this.Dehover();
            navNext = false;
        }
        else if (horizontalInput < -0.5f && OnLeftNaigationElement != null && navNext)
        {
            var temp = OnLeftNaigationElement.GetComponent<NavigationManager>();
            temp.Hover();
            this.Dehover();
            navNext = false;
        }
        else if ((horizontalInput > -0.3f && horizontalInput < 0.3f) && (verticalInput > -0.3f && verticalInput < 0.3f))
        {
            navNext = true;
        }

        INavigationInterface action = this.ActionCollection as INavigationInterface;
        if ((GamePad.GetButtonTrigger(N3dsButton.A) || Input.GetButton("Jump")) && action != null && this.IsInputListenerEnabled && IsPressEnabled)
        {
            audioManager.PlayOnForward();
            action.OnSelect();
            navButton = false;
        }
        else if ((GamePad.GetButtonTrigger(N3dsButton.B) || Input.GetButton("Fire3")) && action != null && this.IsInputListenerEnabled && BackIsEnabled)
        {
            audioManager.PlayOnBack();
            action.OnBack();
            navButton = false;
        }
        else if ((GamePad.GetButtonTrigger(N3dsButton.Start) || Input.GetButton("Fire1")) && action != null && this.IsInputListenerEnabled)
        {
            action.OnStart();
            navButton = false;
        }
        else if ((GamePad.GetButtonRelease(N3dsButton.B) || Input.GetButtonUp("Fire3")) && action != null && this.IsInputListenerEnabled)
        {
            navButton = true;
        }
    }
}
