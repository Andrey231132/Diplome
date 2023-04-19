using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine1 : MonoBehaviour
{
    [SerializeField]
    private Element element;
    [SerializeField]
    private Sprite sprite_element;
    [SerializeField]
    private Transform gameObject_element;

    private void Awake()
    {
        gameObject_element.GetComponent<SpriteRenderer>().sprite = sprite_element;
    }
    public Element GetElement()
    {
        return element;
    }
}
public enum Element
{
    Grass,
    Fire,
    Stone
}
