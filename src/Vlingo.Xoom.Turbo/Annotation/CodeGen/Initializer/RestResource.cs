// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Template;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer;

public class RestResource
{
	private readonly CodeElementFormatter _formatter;
	private readonly string _className;

	private RestResource(CodeElementFormatter formatter, string className)
	{
		_formatter = formatter;
		_className = className;
	}

	public static List<RestResource> From(IReadOnlyList<ContentBase> contents)
	{
		var formatter = ComponentRegistry.WithType<CodeElementFormatter>();
		var classNames = ContentQuery.FindClassNames(contents, new TemplateStandard(TemplateStandardType.RestResource),
			AnnotationBasedTemplateStandard.AutoDispatchResourceHandler);
		return classNames.Select(className => new RestResource(formatter, className)).ToList();
	}
}