// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;
using Vlingo.Xoom.Common;

namespace Vlingo.Xoom.Turbo.Scooter;

public class CompletesEventuallyAdapter : ICompletesEventually
{
    private readonly ICompletes<Type> _completes;

    public CompletesEventuallyAdapter(ICompletes<Type> completes) => _completes = completes;

    public IAddress Address => NoAddress.NoAddressValue;

    public bool IsStopped => throw new NotImplementedException();

    public void Conclude() => throw new NotImplementedException();

    public void Stop() => throw new NotImplementedException();

    public void With(object? outcome) => _completes.With(outcome);
}