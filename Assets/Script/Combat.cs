using UnityEngine;
using System.Collections;

public class Combat : MonoBehaviour
{
    public GameObject target;

    public AnimationClip attack;


    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && target != null)
        {
            transform.LookAt(target.transform.position);
            animation.CrossFade(attack.name);
            ClickToMove.isAttacking = true;
        }

        if (!animation.IsPlaying(attack.name))
        {
            ClickToMove.isAttacking = false;
        }
    }
}
