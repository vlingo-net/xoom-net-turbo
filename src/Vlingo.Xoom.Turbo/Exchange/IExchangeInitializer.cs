// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio.Store.Dispatch;
using IDispatcher = Vlingo.Xoom.Symbio.Store.Dispatch.IDispatcher;

namespace Vlingo.Xoom.Turbo.Exchange;

public abstract class ExchangeInitializer
{
	public abstract void Init(object grid);
	public IDispatcher Dispatcher { get; } = new NoOpDispatcher();
}