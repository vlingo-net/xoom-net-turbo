// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Storage
{
    public class Model
    {
        private readonly string _label;

        public Model(string label) => _label = label;

        public bool IsQueryModel => Equals(ModelType.Query);

        public bool IsDomainModel => Equals(ModelType.Domain);

        public override string ToString() => _label;
    }
}
