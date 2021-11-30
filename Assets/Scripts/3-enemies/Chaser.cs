﻿using UnityEngine;

/**
 * This component chases a given target object.
 */
public class Chaser: TargetMover {
    [Tooltip("The object that we try to chase")]
    [SerializeField] Transform targetObject = null;

    public void setTargetObject(GameObject target)
    {
        targetObject = target.transform;
    }
    public Vector3 TargetObjectPosition() {
        return targetObject.position;
    }

    private void Update() {
        SetTarget(targetObject.position);
    }
}
