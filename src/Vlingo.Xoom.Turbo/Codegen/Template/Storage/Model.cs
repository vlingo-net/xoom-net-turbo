// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Codegen.Template.Storage
{
    public enum ModelType
    {
        Domain,
        Command,
        Query
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
                return new List<ModelType>() { ModelType.Query, ModelType.Command };
            }
            return new List<ModelType>() { ModelType.Domain };
        }

        public bool IsCommandModel() => title == "CommandModel";

        public bool IsQueryModel() => title == "QueryModel";

        public bool IsDomainModel() => title == "DomainModel";
    }
}
