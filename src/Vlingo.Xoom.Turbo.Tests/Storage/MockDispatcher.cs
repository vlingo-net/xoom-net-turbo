// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Xoom.Symbio.Store.Dispatch;

namespace Vlingo.Xoom.Turbo.Tests.Storage
{
  public class MockDispatcher<E, RS> : IDispatcher
  {
    public void ControlWith(IDispatcherControl control)
    {
      throw new System.NotImplementedException();
    }

    public void Dispatch(Dispatchable dispatchable)
    {
      throw new System.NotImplementedException();
    }
  }
}