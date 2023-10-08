using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClickController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    void Update ()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
            Click();
    }

    void Click ()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        // Shoot a raycast from our mouse to what ever we are pointing at.
        if(Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            int hitLayer = hit.collider.gameObject.layer;

            // Did we hit the GROUND?
            if(hitLayer == LayerMask.NameToLayer("Ground"))
            {
                Player.Current.SetTarget(null);
                Player.Current.Controller.MoveToPosition(hit.point);
            }
            // Did we hit an ENEMY?
            else if(hitLayer == LayerMask.NameToLayer("Enemy"))
            {
                Character enemy = hit.collider.GetComponent<Character>();
                Player.Current.SetTarget(enemy);
            }
        }
    }
}