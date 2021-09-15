using System;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence
{
	public class SnapshotState : State<string>
	{
		public SnapshotState(string id, Type type, int typeVersion, string data, int dataVersion, Metadata metadata) :
			base(id, type, typeVersion, data, dataVersion, metadata)
		{
		}

		public SnapshotState(string id, Type type, int typeVersion, string data, int dataVersion) : base(id, type,
			typeVersion, data, dataVersion)
		{
		}
	}
}