// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Annotation.Codegen.Storage
{
	public class PersistenceDetail
	{
		public static string ResolvePackage(string basePackage)
		{
			if (basePackage.EndsWith(".infrastructure"))
				return $"{basePackage}.{PersistencePackageName}";
			
			return string.Format(PackagePattern, basePackage, PackagePattern, PersistencePackageName).ToLower();
		}

		private const string PackagePattern = "{0}.{1}.{2}";
		private const string ParentPackageName = "infrastructure";
		private const string PersistencePackageName = "persistence";
	}
}