// Copyright © 2012-2023 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Xoom.Turbo.Codegen.Template;

using System.IO;

public class OutputFile
{
	private readonly string _absolutePath;
	private readonly string _filename;
	private readonly string _offset;
	private readonly bool _placeholder;

	public OutputFile(string absolutePath, string filename) : this(absolutePath, filename, "", false)
	{
	}

	private OutputFile(string absolutePath, string filename, string offset, bool placeholder)
	{
		_absolutePath = absolutePath;
		_filename = filename;
		_offset = offset;
		_placeholder = placeholder;
	}

	public FileStream ToFile()
	{
		Directory.CreateDirectory(Path.GetDirectoryName(FilePath())!);
		return new FileStream(FilePath(), FileMode.Create);
	}

	private string FilePath() => Path.Combine(_absolutePath, _filename);
}