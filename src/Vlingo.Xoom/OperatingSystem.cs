// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;

namespace Vlingo.Xoom
{
    public class OperatingSystem
    {
        public static OperatingSystemType Detect()
        {
            return isWindows ? OperatingSystemType.WINDOWS : OperatingSystemType.OTHER;
        }

        public static bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public enum OperatingSystemType
    {
        WINDOWS,
        OTHER
    }
}
