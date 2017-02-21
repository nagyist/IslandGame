using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class HexTile : MonoBehaviour, IPointerClickHandler {

    public Hex hex;

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Robber");
    }

}
