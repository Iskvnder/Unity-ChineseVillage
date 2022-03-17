using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDelay : MonoBehaviour
{
    
    IEnumerator OnButtonClicked()
    {
        StartCoroutine(OnButtonClicked());
        yield return new WaitForSeconds(5);
    }
}
