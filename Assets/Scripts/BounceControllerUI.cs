using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BounceControllerUI : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] BounceController bounceController;

    private void OnEnable()
    {
        text.text = "BOUNCES: 0";
        bounceController.onBounceOffGround.AddListener(UpdateUI);
    }

    void UpdateUI()
    {
        text.text = "BOUNCES: " + bounceController.GetBounces().ToString();
    }


}
