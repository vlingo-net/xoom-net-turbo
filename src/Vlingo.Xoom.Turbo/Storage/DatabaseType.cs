// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;

namespace Vlingo.Xoom.Turbo.Storage;

public class DatabaseType
{
    private readonly string _name;

    public DatabaseType(string name) => _name = name;

    public static DatabaseCategory RetrieveFromConfiguration(Configuration? configuration)
    {
        if (configuration == null)
        {
            return DatabaseCategory.InMemory;
        }

        throw new ArgumentException($"Configuration is not supported");
    }

    public bool IsInMemory => Equals(DatabaseCategory.InMemory);

    public bool HasName(string name) => _name == name;
}