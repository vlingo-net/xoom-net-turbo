using System.Collections.Generic;
using Vlingo.Xoom.Turbo.Annotation.Persistence;

namespace Vlingo.Xoom.Turbo.Annotation.Codegen
{
	public class Configuration
	{
		public static Dictionary<StorageType, string> CommandModelStoreTemplates =>
			new Dictionary<StorageType, string>() { { StorageType.StateStore, "StateStoreProvider" }, { StorageType.Journal, "JournalProvider" } };
	}
}