// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Storage;

public class Model
{
    private readonly string _label;

    public Model(string label) => _label = label;

    public bool IsQueryModel => _label.Equals(ModelType.Query.ToString());

    public bool IsDomainModel => _label.Equals(ModelType.Domain.ToString());

    public override string ToString() => _label;
}