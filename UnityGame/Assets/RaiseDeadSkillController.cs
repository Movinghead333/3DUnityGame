using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseDeadSkillController : MonoBehaviour
{
    private GameObject handPrototype;
        
    // Skill properties
    private int numberOfHands = 10;
    private float diameter = 4f;
    private float duration = 5f;

    // internal managment
    private float elapsedTime = 0f;

    private List<GameObject> hands = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        handPrototype = Resources.Load("SkillAssets/Reaper_Spell_RaiseDeadHand") as GameObject;
        for (int i = 0; i < numberOfHands; i++)
        {
            float height = transform.position.y + Random.Range(0f, 0.5f);
            float posAngle = Random.Range(0f, 360f);
            float dist = Random.Range(0f, diameter / 2f);
            float x = Mathf.Cos(posAngle * Mathf.Deg2Rad) * dist;
            float z = Mathf.Sin(posAngle * Mathf.Deg2Rad) * dist;
            Vector3 pos = new Vector3(x, -height, z);

            float rotAngle = Random.Range(0f, 360f);
            Quaternion rot = Quaternion.AngleAxis(rotAngle, Vector3.up);

            hands.Add(Instantiate(handPrototype, pos, rot));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            for(int i = hands.Count - 1; i >= 0; i--)
            {
                GameObject go = hands[i];
                hands.Remove(go);
                Destroy(go);
            }
        }
    }
}
