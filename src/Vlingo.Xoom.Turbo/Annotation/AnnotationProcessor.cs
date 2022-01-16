using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Vlingo.Xoom.Turbo.Codegen.Content;
using Vlingo.Xoom.Turbo.Codegen.Dialect;

namespace Vlingo.Xoom.Turbo.Annotation
{
    public abstract class AnnotationProcessor
    {
        protected ProcessingEnvironment Environment;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Init(ProcessingEnvironment environment)
        {
            this.Environment = environment;
            ComponentRegistry.Register<CodeElementFormatter>(CodeElementFormatter.With(Dialect.C_SHARP));
        }

        public bool Process(ISet<Type> set)
        {
            var annotatedElements = AnnotatedElements.From(SupportedAnnotationClasses());

            if (annotatedElements.Exists)
            {
                try
                {
                    Generate(annotatedElements);
                }
                catch (ProcessingAnnotationException exception)
                {
                    PrintError(exception);
                }
            }

            return true;
        }

        protected abstract void Generate(AnnotatedElements annotatedElements);

        public abstract List<Type> SupportedAnnotationClasses();

        private void PrintError(ProcessingAnnotationException exception)
        {
            Console.WriteLine($"ERROR: {exception.Message}");
        }

        public ISet<string> GetSupportedAnnotationTypes()
        {
            return SupportedAnnotationClasses()
                .Select(type => type.Name)
                .ToImmutableHashSet();
        }
    }
}