// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Annotation.AutoDispatch
{
    public class HandlerEntry<TState> where TState : IHandler
    {
        public readonly int index;
        public readonly TState handler;

        public static HandlerEntry<TState> Of(int index, TState handler)
        {
            return new HandlerEntry<TState>(index, handler);
        }

        private HandlerEntry(int index, TState handler)
        {
            this.index = index;
            this.handler = handler;
        }
    }
}
