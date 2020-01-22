﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverInjector : MonoBehaviour
{
    public ADriver Driver;

    void Awake ()
    {
        foreach (IDriverSubscriber subscriber in FindObjectsOfType<MonoBehaviour>().OfType<IDriverSubscriber>())
        {
            subscriber.Driver = Driver;
        }
    }
}
