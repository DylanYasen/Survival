using UnityEngine;
using System.Collections;

public class Bonfire : HeatSource
{
    void Start()
    {

    }

    void Update()
    {

    }

    public override void Die()
    {
        //base.Die();

        // only put off fire
        // don't destroy bonfire

        return;
    }
}
