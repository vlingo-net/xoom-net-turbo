// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Codegen.Parameter;

namespace Vlingo.Xoom.Turbo.Annotation.Persistence
{
	public class PersistenceParameterResolver
	{
		public static PersistenceParameterResolver From(Type persistenceSetupClass, ProcessingEnvironment environment)
		{
			throw new NotImplementedException();
		}

		public IReadOnlyDictionary<Label,string> Resolve()
		{
			throw new NotImplementedException();
		}
	}
}