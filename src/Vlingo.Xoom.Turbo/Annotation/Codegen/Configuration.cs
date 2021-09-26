// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Annotation.Codegen.Storage;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen
{
	public class Configuration
	{
		public static Dictionary<StorageType, string> QueryModelStoreTemplates =>
			new Dictionary<StorageType, string>() { { StorageType.StateStore, "StateStoreProvider" }, { StorageType.Journal, "StateStoreProvider" } };
		public static Dictionary<StorageType, string> CommandModelStoreTemplates =>
			new Dictionary<StorageType, string>() { { StorageType.StateStore, "StateStoreProvider" }, { StorageType.Journal, "JournalProvider" } };
	}
}