using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hon : Enemy

{
    bool interactingWithPlayer;
    Vector3 _posToLean;
    Quaternion _rotToLean;
    public void GoAttack(Transform _pos)
    {
        
        _posToLean = _pos.position;
        _rotToLean = _pos.rotation;
        interactingWithPlayer = true;
        Nav.enabled = false;
        enableTree("nothing");
        anim.SetTrigger("Disappear");
    }
    public void Appear()
    {
        anim.SetTrigger("Appear");
        WaitAndSwap();
    }
    override public  void Update()
    {
        //anim.MatchTarget(matchPosition.Value, matchRotation.Value, targetBodyPart, new MatchTargetWeightMask(weightMaskPosition, weightMaskRotation), startNormalizedTime, targetNormalizedTime);
    }
    private IEnumerator WaitAndSwap()
    {
            yield return new WaitForSeconds(1f);
            transform.rotation = _rotToLean;
            transform.position = _posToLean;
    }
}

	
