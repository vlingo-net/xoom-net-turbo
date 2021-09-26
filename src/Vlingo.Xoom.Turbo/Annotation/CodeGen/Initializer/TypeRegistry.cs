// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using System.Linq;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Initializer
{
	public class TypeRegistry
	{
		private readonly string _className;
		private readonly string _objectName;

		private TypeRegistry(string className, string objectName)
		{
			_className = className;
			_objectName = objectName;
		}

		public static List<TypeRegistry> From(StorageType storageType, bool useCqrs) => storageType
			.FindRelatedStorageTypes(useCqrs)
			.Select(relatedStorageType =>
				new TypeRegistry(relatedStorageType.TypeRegistryClassName(), relatedStorageType.TypeRegistryObjectName()))
			.ToList();
	}
}