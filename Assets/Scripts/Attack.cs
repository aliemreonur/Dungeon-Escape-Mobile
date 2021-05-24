using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _hitted = false;
    //this is for making sure that we do not hit an enemy multiple times on one swing

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        //if the object that we hit has an IDamageable interface, it will be stored in hit

        if( hit!=null)
        {
            if(!_hitted)
            {
                hit.Damage();
                //this will apply the Damage method in the object that we have hit
                _hitted = true;
                StartCoroutine(CooldownRoutine());
            }
        }
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(1f);
        _hitted = false;
    }

}
