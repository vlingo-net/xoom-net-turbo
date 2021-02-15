// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Xoom.Codegen.Template.Storage
{
    public enum ModelType
    {
        DOMAIN,
        COMMAND,
        QUERY
    }

    public class Model
    {
        public readonly string title;

        public Model(string title)
        {
            this.title = title;
        }

        public static IEnumerable<ModelType> ApplicableFor(bool useCQRS)
        {
            if (useCQRS)
            {
                return new List<ModelType>() { ModelType.QUERY, ModelType.COMMAND };
            }
            return new List<ModelType>() { ModelType.DOMAIN };
        }

        public bool IsCommandModel() => title == "CommandModel";

        public bool IsQueryModel() => title == "QueryModel";

        public bool IsDomainModel() => title == "DomainModel";
    }
}
