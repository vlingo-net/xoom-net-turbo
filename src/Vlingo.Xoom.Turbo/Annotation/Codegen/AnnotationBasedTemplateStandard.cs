// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;
using Vlingo.Xoom.Turbo.Annotation.Persistence;
using Vlingo.Xoom.Turbo.Codegen.Template;
using Model = Vlingo.Xoom.Turbo.Storage.Model;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen
{
	public class AnnotationBasedTemplateStandard : TemplateStandard
	{
		public static TemplateStandard StoreProvider => new TemplateStandard((parameters) =>
		{
			var storageType = parameters.Find<StorageType>(TemplateParameter.StorageType);
			if(parameters.Find<Model>(TemplateParameter.Model).IsQueryModel)
				return Configuration.QueryModelStoreTemplates[storageType];
			return Configuration.CommandModelStoreTemplates[storageType];
		});

		public static TemplateStandard AutoDispatchResourceHandler => new TemplateStandard((parameters) => "RestResource");
		public static TemplateStandard AggregateState => new TemplateStandard((parameters) => null);
		public static TemplateStandard XoomInitializer => new TemplateStandard((parameters) => "", (name, parameters)
			=> "XoomInitializer");

		public static TemplateStandard ProjectionDispatcherProvider  => new TemplateStandard((parameters) => "ProjectionDispatcherProvider", (name, parameters)
			=> "ProjectionDispatcherProvider");
	}
}