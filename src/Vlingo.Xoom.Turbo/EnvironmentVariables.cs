// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Turbo
{
    public class EnvironmentVariables
    {
        public static string Retrieve(string key)
        {
            RegisterDefaultRetrieverIfNotRegistered();

            return ComponentRegistry
                .WithType<IEnvironmentVariablesRetriever>()
                .Retrieve(key);
        }

        public static bool ContainsKey(string key)
        {
            RegisterDefaultRetrieverIfNotRegistered();

            return ComponentRegistry
                .WithType<IEnvironmentVariablesRetriever>()
                .ContainsKey(key);
        }

        private static void RegisterDefaultRetrieverIfNotRegistered()
        {
            if (!ComponentRegistry.Has<IEnvironmentVariablesRetriever>())
            {
                ComponentRegistry.Register<IEnvironmentVariablesRetriever>(new DefaultRetriever());
            }
        }

        public interface IEnvironmentVariablesRetriever
        {
            string Retrieve(string key);
            bool ContainsKey(string key);
        }


        public class DefaultRetriever : IEnvironmentVariablesRetriever
        {
            public string Retrieve(string key) => Environment.GetEnvironmentVariable(key) ?? string.Empty;

            public bool ContainsKey(string key) => Environment.GetEnvironmentVariables().Contains(key);
        }
    }
}