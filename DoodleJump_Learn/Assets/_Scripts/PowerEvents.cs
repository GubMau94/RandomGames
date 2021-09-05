using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEvents : MonoBehaviour
{
    private static Animator _anim;
    public static bool animEnd;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void JetpackAnimEnd()
    {
        animEnd = true;
    }

    public void PropellerAnimEnd()
    {
        animEnd = true;
    }

    public void RocketAnimEnd()
    {
        animEnd = true;
    }

    public static void SpringShoesAnim()
    {
        _anim.SetBool("SpringActive", true);
    }

    public void SpringEffectOff()
    {
        _anim.SetBool("SpringActive", false);
    }

}
