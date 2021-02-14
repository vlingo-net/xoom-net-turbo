// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors;

namespace Vlingo.Xoom
{
    public class Boot
    {

        private static World? xoomBootWorld;

        public static void Main(string[] args)
        {
            string name = args.Length > 0 ? args[0] : "vlingo-xoom";

            xoomBootWorld = Start(name);
        }

        public static World? XoomBootWorld()
        {
            return xoomBootWorld;
        }

        /// <summary>
        /// Answers a new <see cref="World"/> with the given name and that is configured with the contents of the <see langword="Vlingo-Zoom.Properties"/> file.
        /// </summary>
        /// <param name="@name"> the <see cref="string"/> @name to assign to the new <see cref="World"/> instance</param>
        /// <returns><see cref="World"/></returns>
        public static World Start(string name)
        {
            xoomBootWorld = World.Start(name);
            return xoomBootWorld;
        }
    }
}
