// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Annotation.AutoDispatch
{
    public interface IHandler
    {
        interface Two<A, B> where A : IHandler where B : IHandler
        {
            A Handle(B b);
        }

        interface Three<A, B, C> where A : IHandler where B : IHandler where C : IHandler
        {
            A Handle(B b, C c);
        }

        interface Four<A, B, C, D> where A : IHandler where B : IHandler where C : IHandler where D : IHandler
        {
            A Handle(B b, C c, D d);
        }

        interface Five<A, B, C, D, E> where A : IHandler where B : IHandler where C : IHandler where D : IHandler where E : IHandler
        {
            A Handle(A a, B b, C c, D d, E e);
        }
    }
}
