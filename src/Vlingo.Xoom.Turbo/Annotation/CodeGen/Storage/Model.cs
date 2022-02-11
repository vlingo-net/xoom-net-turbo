// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

public enum ModelType
{
    Domain,
    Command,
    Query
}

public static class ModelTypeExtensions
{
    public static IEnumerable<ModelType> ApplicableTo(bool useCqrs)
    {
        if (useCqrs)
        {
            return new List<ModelType>() { ModelType.Query, ModelType.Command };
        }
        return new List<ModelType>() { ModelType.Domain };
    }

    public static bool IsCommandModel(this ModelType modelType) => modelType.Equals(ModelType.Command);

    public static bool IsQueryModel(this ModelType modelType) => modelType.Equals(ModelType.Query);

    public static bool IsDomainModel(this ModelType modelType) => modelType.Equals(ModelType.Domain);
    public static bool RequireAdapter(this ModelType modelType) => !modelType.IsQueryModel();
        
}