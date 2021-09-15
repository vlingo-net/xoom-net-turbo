// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

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