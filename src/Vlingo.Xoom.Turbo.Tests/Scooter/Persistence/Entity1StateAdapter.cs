// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Common.Serialization;
using Vlingo.Xoom.Symbio;

namespace Vlingo.Xoom.Turbo.Tests.Scooter.Persistence;

public class Entity1StateAdapter : IStateAdapter<Entity1, State<string>>
{
	public object ToRawState<T>(T state, int stateVersion, Metadata metadata) =>
		ToRawState((state as Entity1)?.Id, stateVersion, metadata);

	public Entity1 FromRawState(State<string> raw) => JsonSerialization.Deserialized<Entity1>(raw.Data);

	public TOtherState FromRawState<TOtherState>(State<string> raw) =>
		JsonSerialization.Deserialized<TOtherState>(raw.Data);

	public State<string> ToRawState(string id, Entity1 state, int stateVersion, Metadata metadata)
	{
		var serialization = JsonSerialization.Serialized(state);
		return new TextState(id, typeof(Entity1), TypeVersion, serialization, stateVersion);
	}

	public State<string> ToRawState(Entity1 state, int stateVersion, Metadata metadata) =>
		ToRawState(state.Id, state, stateVersion, metadata);

	public State<string> ToRawState(Entity1 state, int stateVersion) =>
		ToRawState(state, stateVersion, Metadata.NullMetadata());

	public int TypeVersion { get; } = 1;
}