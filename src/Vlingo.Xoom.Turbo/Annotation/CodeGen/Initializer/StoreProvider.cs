// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer
{
	public class StoreProvider
	{
		private readonly string _className;
		private readonly string _arguments;

		private StoreProvider(StorageType storageType, ModelType model, bool useProjection, bool hasExchange)
		{
			var parameters = TemplateParameters.With(TemplateParameter.StorageType, storageType)
				.And(TemplateParameter.Model, model);

			_className = AnnotationBasedTemplateStandard.StoreProvider.ResolveClassname(parameters);
			_arguments = ResolveArguments(model, storageType, useProjection, hasExchange);
		}

		private string ResolveArguments(ModelType model, StorageType storageType, bool useProjection, bool hasExchange)
		{
			var typeRegistryObjectName = storageType.ResolveTypeRegistryObjectName(model);
			var exchangeDispatcherAccess = hasExchange ? "exchangeInitializer.Dispatcher()" : "";
			var projectionDispatcher =
				$"{AnnotationBasedTemplateStandard.ProjectionDispatcherProvider.ResolveClassname()}.Using(_grid.World.Stage).StoreDispatcher";

			var arguments = new List<string>() { "_grid.World.Stage", typeRegistryObjectName };
			if (!model.IsQueryModel())
			{
				if (useProjection)
				{
					arguments.Add(projectionDispatcher);
				}
				arguments.Add(exchangeDispatcherAccess);
			}

			return string.Join(", ", arguments.Where(arg => !string.IsNullOrEmpty(arg)));
		}

		public static List<StoreProvider> From(StorageType storageType, bool useCqrs, bool useProjection, bool hasExchange)
		{
			if (storageType.Equals(StorageType.None))
			{
				return new List<StoreProvider>();
			}

			return ModelTypeExtensions.ApplicableTo(useCqrs)
				.Select(model => new StoreProvider(storageType, model, useProjection, hasExchange))
				.ToList();
		}
	}
}