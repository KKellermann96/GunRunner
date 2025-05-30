﻿using log4net.Config;
using System.IO;
using UnityEngine;

public static class LoggingConfiguration
{
   [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
   private static void Configure()
    {
        XmlConfigurator.Configure(new FileInfo(fileName: $"{Application.dataPath}/log4net.xml"));
    }
}
