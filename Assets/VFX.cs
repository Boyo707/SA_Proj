using UnityEngine;
using TMPro;

public class VFX : MonoBehaviour
{
    [SerializeField] TMP_Text VFXText;
    public void SetVFX(string pText, Color pColor)
    {
        VFXText.text = pText;
        VFXText.color = pColor;
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
