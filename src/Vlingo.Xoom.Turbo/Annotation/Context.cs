// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.IO;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public class Context
	{
		private static readonly string TestOutputDirectory = "test-classes";
		
		public static string LocateBaseDirectory(string path)
		{
			var ancestral = LocateOutputFolder(path);
			while (!ancestral.Name.Equals("target"))
			{
				ancestral = ancestral.Parent;
			}
			
			return ancestral.FullName;
		}

		public static object LocateSourceFolder(string path)
		{
			var baseDirectory = LocateBaseDirectory(path);
			var parentFolder = IsRunningTests(path) ? "test" : "main";
			return Path.Combine(baseDirectory, "src", parentFolder, "cs");
		}
		
		internal static bool IsRunningTests(string path)
		{
            var directoryName = LocateOutputFolder(path);
            return directoryName.Name.Equals(TestOutputDirectory);
		}
		
		private static DirectoryInfo LocateOutputFolder(string path)
		{
			try
			{
				return Directory.GetParent(path);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
				throw new ProcessingAnnotationException("Unable to locate the output folder");
			}
		}
	}
}