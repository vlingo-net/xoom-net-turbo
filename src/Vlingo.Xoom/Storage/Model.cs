// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Storage
{
    public class Model
    {
        private string label;

        public Model(string label)
        {
            this.label = label;
        }

        public bool IsQueryModel()
        {
            return this.Equals(ModelType.QUERY);
        }

        public bool IsDomainModel()
        {
            return this.Equals(ModelType.DOMAIN);
        }

        public override string ToString()
        {
            return label;
        }
    }

    public enum ModelType
    {
        DOMAIN,
        COMMAND,
        QUERY
    }
}
