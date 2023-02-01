// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Codegen.Content;

public class CodeElementFormatter
{
	private readonly Dialect.Dialect _dialect;

	private CodeElementFormatter(Dialect.Dialect dialect) => _dialect = dialect;

	public static CodeElementFormatter With(Dialect.Dialect dialect) => new CodeElementFormatter(dialect);

	public string SimpleNameToAttribute(string simpleName) => simpleName;

	public string SimpleNameOf(string qualifiedName) =>
		qualifiedName.Substring(qualifiedName.LastIndexOf(".", StringComparison.Ordinal) + 1);

	public string QualifiedNameOf(string packageName, string className) => packageName + "." + className;
}