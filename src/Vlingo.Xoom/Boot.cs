// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors;

namespace Vlingo.Xoom {
    public class Boot {

        private static World xoomBootWorld;

        public static void Main(string[] args) {
            string name = args.Length > 0 ? args[0] : "vlingo-xoom";

            xoomBootWorld = Start(name);
        }

        public static World XoomBootWorld() {
            return xoomBootWorld;
        }

        /**
         * Answers a new {@code World} with the given {@code name} and that is configured with
         * the contents of the {@code vlingo-zoom.properties} file.
         * @param name the {@code String} name to assign to the new {@code World} instance
         * @return {@code World}
         */
        public static World Start(string name) {
            xoomBootWorld = World.Start(name);

            return xoomBootWorld;
        }

    }
}
