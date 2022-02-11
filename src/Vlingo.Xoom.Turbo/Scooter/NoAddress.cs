// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Actors;

namespace Vlingo.Xoom.Turbo.Scooter;

public class NoAddress : IAddress
{
    public static readonly IAddress NoAddressValue = new NoAddress();

    public int CompareTo(IAddress? other) => -1;

    public long Id => 0;

    public long IdSequence => 0;

    public string IdSequenceString => "0";

    public string IdString => "0";

    public T IdTyped<T>(Func<string, T> typeConverter) => default!;

    public string Name => string.Empty;

    public bool IsDistributable => false;
}