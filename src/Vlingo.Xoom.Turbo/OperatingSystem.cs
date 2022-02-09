// Copyright © 2012-2022 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices;

namespace Vlingo.Xoom.Turbo
{
    public class OperatingSystem
    {
        public static OperatingSystemType Detect() => IsWindows ? OperatingSystemType.Windows : OperatingSystemType.Other;

        public static bool IsWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    }

    public enum OperatingSystemType
    {
        Windows,
        Other
    }
}
