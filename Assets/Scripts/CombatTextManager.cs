using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManager : MonoBehaviour
{
    public static float health;

    public GameObject textPrefab;

    public static CombatTextManager instance;

    public RectTransform canvasTransform;

    public float speed;

    public Vector3 direction;

    public Transform camTransform;

    public float fadeTime;

    public static CombatTextManager Instance
    {
        get
        {
            if(instance==null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManager>();
            }
            return instance;
        }
    }

    public void CreateText(Vector3 position, string text, Color color)
    {
        GameObject sct = (GameObject)Instantiate(textPrefab, position, Quaternion.identity);

        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        sct.GetComponent<CombatText>().Initialize(speed, direction, fadeTime);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
    }
}
