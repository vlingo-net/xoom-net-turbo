// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage
{
	public class Adapter
	{
		private readonly string _sourceClass;
		private readonly string _adapterClass;
		private readonly bool _last;

		private Adapter(int index, int numberOfAdapters, TemplateParameters parameters) : 
			this(index, numberOfAdapters, parameters.Find<string>(TemplateParameter.SourceName), parameters.Find<string>(TemplateParameter.AdapterName))
		{
		}

		private Adapter(int index, int numberOfAdapters, string sourceClass, string adapterClass)
		{
			_sourceClass = sourceClass;
			_adapterClass = adapterClass;
			_last = index == numberOfAdapters - 1;
		}

		public static List<Adapter> From(List<TemplateData> templatesData)
		{
			Func<TemplateData, bool> filter = data =>
				data.HasStandard(TemplateStandardType.Adapter);
			
			var adapterTemplates = templatesData.Where(filter)
				.ToList();
			return Enumerable.Range(0, templatesData.Count)
				.Select(index =>
				{
					var templateData = templatesData.ElementAt(index);
					return new Adapter(index, adapterTemplates.Count, templateData.Parameters());
				})
				.ToList();
		}
	}
}