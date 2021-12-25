// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Vlingo.Xoom.Turbo.Annotation
{
	public class TypeReader
	{
		private const string DefaultMatchPattern = " {0} ";
		private const string VariableMatchPattern = " {0}=";

		private readonly Type _type;
		private readonly string _sourceCode;

		public static TypeReader From(ProcessingEnvironment environment, Type typeElement)
		{
			return new TypeReader(environment, typeElement);
		}

		private TypeReader(ProcessingEnvironment environment, Type type)
		{
			this._type = type;
			this._sourceCode = ReadSourceCode(environment.GetFiler(), type);
		}

		public string FindMemberValue(MemberInfo member)
		{
			var memberName = member.Name;
			if (!HasMember(memberName))
			{
				throw InvalidMemberArgumentException(memberName);
			}

			return ExtractMemberValue(memberName);
		}

		public string FindMemberValue(string memberName)
		{
			return FindMembers().Select(member => member.Name)
				.Where(name => name == memberName)
				.Select(member => ExtractMemberValue(memberName))
				.FirstOrDefault() ?? throw InvalidMemberArgumentException(memberName);
		}

		public List<MemberInfo> FindMembers()
		{
			return _type
				.GetMembers()
				.OfType<MemberInfo>()
				.ToList();
		}

		private string ExtractMemberValue(string memberName)
		{
			var elementIndex = CodeElementIndex(memberName, new[] { DefaultMatchPattern, VariableMatchPattern });
			var valueIndex = elementIndex + memberName.Count() + 1;
			var codeSlice = _sourceCode.Substring(valueIndex).Replace("=", "").Trim();
			return codeSlice.Substring(0, codeSlice.IndexOf(";")).Trim();
		}

		private string ReadSourceCode(FileStream filer, Type typeElement)
		{
			try
			{
				var stream = ClassFile.From(filer, typeElement).OpenInputStream();

				return File.ReadAllText(new StreamReader(stream).ReadLine(), Encoding.UTF8).Replace("\r\n", " ");
			}
			catch (IOException e)
			{
				throw new ProcessingAnnotationException(e);
			}
		}

		private ArgumentException InvalidMemberArgumentException(string memberName)
		{
			return new ArgumentException("Member " + memberName + " not found in " + _type.AssemblyQualifiedName);
		}

		private bool HasMember(string memberName)
		{
			return _type.GetMembers()
				.Where(element => element is MemberInfo)
				.Select(element => element.Name)
				.Any(name => name == memberName);
		}

		private int CodeElementIndex(string elementName, string[] patterns)
		{
			return patterns.Select(pattern => String.Format(pattern, elementName))
				.Where(term => _sourceCode.Contains(term))
				.Select(term => _sourceCode.IndexOf(term))
				.FirstOrDefault();
		}
	}
}