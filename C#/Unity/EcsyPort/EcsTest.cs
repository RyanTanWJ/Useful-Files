﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EcsTest : MonoBehaviour
{
    [SerializeField]
    private GameObject testCube;
    EcsyPort.World world;
    
    void Start()
    {
        world = new EcsyPort.World();
        world.registerSystem(new TestPort.MovementSystem());
        world.registerSystem(new TestPort.RotationSystem());
        world.requestEntity<TestPort.CubeEntity>();
    }

    void Update()
    {
        world.execute(Time.deltaTime);
        TestPort.CubeEntity cubeEntity = (TestPort.CubeEntity) world.getEntity<TestPort.CubeEntity>(0);
        testCube.transform.position = new Vector3(cubeEntity.Position.x, cubeEntity.Position.y, cubeEntity.Position.z);
        testCube.transform.rotation = Quaternion.Euler(cubeEntity.Rotation.x, cubeEntity.Rotation.y, cubeEntity.Rotation.z);
    }
}
