using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image healthFill;
    [SerializeField] private Character character;

    void OnEnable ()
    {
        character.onTakeDamage += UpdateHealthBar;
    }

    void OnDisable ()
    {
        character.onTakeDamage -= UpdateHealthBar;
    }

    void Start ()
    {
        UpdateHealthBar();
    }

    void Update ()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }

    void UpdateHealthBar ()
    {
        healthFill.fillAmount = (float)character.CurHp / (float)character.MaxHp;
    }
}