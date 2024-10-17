using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderClickDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        // selectAnimationRing.SetActive(true);
        BubtSceneManager.Instance.OnBoxClicked(gameObject);
    }
}
