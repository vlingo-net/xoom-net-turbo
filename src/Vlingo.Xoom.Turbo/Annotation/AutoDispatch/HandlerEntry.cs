// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch;

public class HandlerEntry
{
    public int Index { get; }
    public Delegate Handler { get; }

    public static HandlerEntry Of<TB, TResult>(int index, Func<TB, TResult> handler) => new HandlerEntry(index, handler);
    public static HandlerEntry Of<TB, TC, TResult>(int index, Func<TB, TC, TResult> handler) => new HandlerEntry(index, handler);
    public static HandlerEntry Of<TB, TC, TD, TResult>(int index, Func<TB, TC, TD, TResult> handler) => new HandlerEntry(index, handler);
    public static HandlerEntry Of<TB, TC, TD, TE, TResult>(int index, Func<TB, TC, TD, TE, TResult> handler) => new HandlerEntry(index, handler);

    private HandlerEntry(int index, Delegate handler)
    {
        Index = index;
        Handler = handler;
    }
}