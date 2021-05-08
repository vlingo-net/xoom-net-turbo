//// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace Vlingo.Xoom.Annotation.AutoDispatch
//{
//    public class HandlerResolver
//    {
//        private static string _handlerEntryClassName = typeof(HandlerEntry<>).Name;

//        private readonly TypeReader _handlersConfigReader;
//        private readonly List<HandlerInvocation> _handlerInvocations = new List<HandlerInvocation>();

//        public static HandlerResolver With(TypeElement handlersConfig, ProcessingEnvironment environment) => new HandlerResolver(handlersConfig, environment);

//        private HandlerResolver(TypeElement handlersConfig, ProcessingEnvironment environment)
//        {
//            _handlersConfigReader = TypeReader.from(environment, handlersConfig);
//            _handlerInvocations.AddRange(resolveInvocations());
//        }

//        public HandlerInvocation Find(int index) => _handlerInvocations.FirstOrDefault(invocation => invocation.index == index) ?? throw new ArgumentException(string.Concat("Handler Invocation with index ", index, " not found"));

//        private List<HandlerInvocation> resolveInvocations()
//        {
//            final Predicate<Element> onlyHandlerEntries =
//                    element->element.asType().toString().startsWith(HANDLER_ENTRY_CLASSNAME);

//            return handlersConfigReader.findMembers().stream().filter(onlyHandlerEntries)
//                    .map(handlerEntry-> new HandlerInvocation(handlersConfigReader, handlerEntry))
//                    .collect(Collectors.toList());

//        }
//    }
//}
