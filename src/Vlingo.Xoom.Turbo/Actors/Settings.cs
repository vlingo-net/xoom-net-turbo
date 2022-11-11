// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Actors;

public class Settings : ConfigurationProperties
{
    public static Properties Properties { get; } = new Properties();
    private const string PropertiesFileName = "/xoom-turbo.json";

    static Settings() => LoadProperties();

    private static void LoadProperties()
    {
        try
        {
            Properties.Load(new FileInfo(PropertiesFileName));
        }
        catch (IOException e)
        {
            throw new PropertiesLoadingException(e.Message, e);
        }
    }
}