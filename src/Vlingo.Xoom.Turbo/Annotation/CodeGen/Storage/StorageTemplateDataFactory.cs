// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Projections;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage
{
	public class StorageTemplateDataFactory
	{
		public static List<TemplateData> Build(string basePackage, IReadOnlyList<ContentBase> contents, StorageType storageType, ProjectionType projectionType, bool useCqrs)
		{
			var persistencePackage = PersistenceDetail.ResolvePackage(basePackage);
			var templatesData = new List<TemplateData>();
			templatesData.Add(AdapterTemplateData.From(persistencePackage, storageType, contents));
			templatesData.Add(StorageProviderTemplateData.From(persistencePackage, storageType, projectionType, 
				templatesData, contents, ModelTypeExtensions.ApplicableTo(useCqrs)));

			return templatesData;
		}
	}
}