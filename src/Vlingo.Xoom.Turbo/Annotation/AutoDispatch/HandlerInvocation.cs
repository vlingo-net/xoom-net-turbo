// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Xoom.Turbo.Annotation;

namespace Vlingo.Xoom.Annotation.AutoDispatch
{
    public class HandlerInvocation
    {
        private static readonly string DefaultHandlerInvocationPattern = "{0}.Handler.Handle";
        private static readonly string ParametrizedHandlerInvocationPattern = DefaultHandlerInvocationPattern + "({0})";

        public readonly int Index;
        public readonly string Invocation;
        private readonly TypeReader _handlersConfigReader;

        public HandlerInvocation(TypeReader handlersConfigReader, Type handlerEntry)
        {
            _handlersConfigReader = handlersConfigReader;
            Index = FindIndex(handlerEntry);
            Invocation = ResolveInvocation(handlerEntry);
        }

        private int FindIndex(Type handlerEntry)
        {
            var handlerEntryValue = _handlersConfigReader.FindMemberValue(handlerEntry);

            var handlerEntryIndex = handlerEntryValue.Substring(handlerEntryValue.IndexOf("(") + 1, handlerEntryValue.IndexOf(","));
            try
            {
                return int.Parse(handlerEntryIndex);
            }
            catch (FormatException)
            {
                return int.Parse(_handlersConfigReader.FindMemberValue(handlerEntryIndex));
            }
        }

        private string ResolveInvocation(Type handlerEntry)
        {
            var handlerEntryValue = _handlersConfigReader.FindMemberValue(handlerEntry);

            if (handlerEntryValue.Contains("->"))
            {
                var handlerInvocationArguments = ExtractHandlerArguments(handlerEntryValue);
                return string.Format(ParametrizedHandlerInvocationPattern,
                        handlerEntry.FullName, handlerInvocationArguments);
            }

            return string.Format(DefaultHandlerInvocationPattern, handlerEntry.FullName);
        }

        private string ExtractHandlerArguments(string handlerEntryValue) => handlerEntryValue.Substring(handlerEntryValue.IndexOf(",") + 1, handlerEntryValue.IndexOf("->")).Replace("\\(", "").Replace("\\)", "").Trim();

        public bool HasCustomParamNames() => Invocation.Contains("(") && Invocation.Contains(")");
    }
}
