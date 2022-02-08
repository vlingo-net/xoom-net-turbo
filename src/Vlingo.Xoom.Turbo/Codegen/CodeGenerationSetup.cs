// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Codegen
{
    public class CodeGenerationSetup
    {
        public static IReadOnlyDictionary<StorageType, string> AggreageteTemplates = new List<(StorageType, string)>()
        {
            (StorageType.ObjectStore, Template.Template.ObjectEntity.ToString()),
            (StorageType.StateStore, Template.Template.StatefulEntity.ToString()),
            (StorageType.Journal, Template.Template.EventSourceEntity.ToString())
        }.ToDictionary(x => x.Item1, x => x.Item2);

        public static readonly IReadOnlyDictionary<StorageType, string> AggregateMethodTempalates =
            new List<(StorageType, string)>()
            {
                (StorageType.ObjectStore, string.Empty),
                (StorageType.StateStore, Template.Template.StatefulEntityMethod.ToString()),
                (StorageType.Journal, Template.Template.EventSourceEntityMethod.ToString())
            }.ToDictionary(x => x.Item1, x => x.Item2);

        public static readonly IReadOnlyDictionary<StorageType, string> AdapterTemplates =
            new List<(StorageType, string)>()
            {
                (StorageType.ObjectStore, string.Empty),
                (StorageType.StateStore, Template.Template.StateAdapter.ToString()),
                (StorageType.Journal, Template.Template.EntryAdapter.ToString())
            }.ToDictionary(x => x.Item1, x => x.Item2);

        //TODO: T4 files will be generated
        //    public static readonly IReadOnlyDictionary<ProjectionType, string> projectionTemplates = new List<(ProjectionType, string)>()
        //{
        //    (ProjectionType.EVENT_BASED, Template.Template.EventBasedProjection.ToString()),
        //    (ProjectionType.OPERATION_BASED, Template.Template.OperationBasedProjection.ToString())
        //}.ToDictionary(x => x.Item1, x => x.Item2);

        private static readonly IReadOnlyDictionary<StorageType, string> CommandModelStoreTemplates =
            new List<(StorageType, string)>()
            {
                (StorageType.ObjectStore, Template.Template.ObjectStoreProvider.ToString()),
                (StorageType.StateStore, Template.Template.StateStoreProvider.ToString()),
                (StorageType.Journal, Template.Template.JournalProvider.ToString())
            }.ToDictionary(x => x.Item1, x => x.Item2);

        private static readonly IReadOnlyDictionary<StorageType, string> QueryModelStoreTemplates =
            new List<(StorageType, string)>()
            {
                (StorageType.ObjectStore, Template.Template.StateStoreProvider.ToString()),
                (StorageType.StateStore, Template.Template.StateStoreProvider.ToString()),
                (StorageType.Journal, Template.Template.StateStoreProvider.ToString())
            }.ToDictionary(x => x.Item1, x => x.Item2);

        public static IReadOnlyDictionary<StorageType, string> StoreProviderTemplatesFrom(ModelType model) =>
            model.IsQueryModel() ? QueryModelStoreTemplates : CommandModelStoreTemplates;
    }
}