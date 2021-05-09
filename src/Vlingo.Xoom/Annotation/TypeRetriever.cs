// Copyright © 2012-2021 VLINGO LABS. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;

namespace Vlingo.Xoom.Annotation
{
    public class TypeRetriever
    {

        private static TypeRetriever _instance;
        private readonly Type _elements;
        private readonly ProcessingEnvironment _environment;

        private TypeRetriever(ProcessingEnvironment environment)
        {
            _environment = environment;
            _elements = environment.GetElementUtils();
        }

        //public IEnumerable<Type> SubclassesOf(object superclass, string[] packages)
        //{
        //    return Stream.of(packages).filter(this::isValidPackage)
        //            .map(packageName->elements.getPackageElement(packageName))
        //            .flatMap(packageElement->packageElement.getEnclosedElements().stream())
        //            .filter(element->isSubclass(element, superclass))
        //            .map(element->element.asType());

        //    var a = packages.Where(x => IsValidPackage(x)).SelectMany(packageName => _elements.GetMembers()).Where(element => elem)
        //}

        //private bool IsSubclass(Type typeElement, object superclass)
        //{
        //    Type type = _elementstyp((string)superclass)
        //                    .asType();

        //    return ((TypeElement)typeElement).getSuperclass()
        //            .equals(type);
        //}

        //public <T> TypeElement from(final T annotation, final Function<T, Class<?>> retriever)
        //{
        //    try
        //    {
        //        final Class<?> clazz =
        //                retriever.apply(annotation);

        //        return environment.getElementUtils()
        //                .getTypeElement(clazz.getCanonicalName());
        //    }
        //    catch (final MirroredTypeException exception) {
        //        return (TypeElement)environment.getTypeUtils()
        //                .asElement(exception.getTypeMirror());
        //    }
        //}

        //public <T>  List<TypeElement> typesFrom(final T annotation, final Function< T, Class <?>[] > retriever) {
        //        try
        //        {
        //            final Class<?>[] classes =
        //            retriever.apply(annotation);

        //            return Stream.of(classes).map(clazz->environment.getElementUtils()
        //                    .getTypeElement(clazz.getCanonicalName())).collect(Collectors.toList());
        //        }
        //        catch (final MirroredTypesException exception) {
        //            return exception.getTypeMirrors().stream()
        //                    .map(typeMirror-> (TypeElement) environment.getTypeUtils()
        //                            .asElement(typeMirror)).collect(Collectors.toList());
        //        }
        //}

        public static TypeRetriever With(ProcessingEnvironment environment) => new TypeRetriever(environment);

        //        public boolean isAnInterface(final Annotation annotation, final Function< Object, Class <?>> retriever) {
        //            return getTypeElement(annotation, retriever).getKind().isInterface();
        //        }

        //        public String getClassName(final Annotation annotation, final Function< Object, Class <?>> retriever) {
        //            return getTypeElement(annotation, retriever).getQualifiedName().toString();
        //        }

        //        public List<ExecutableElement> getMethods(final Annotation annotation, final Function< Object, Class <?>> retriever) {
        //            return (List<ExecutableElement>)getTypeElement(annotation, retriever).getEnclosedElements();
        //        }

        //        public TypeElement getGenericType(final Annotation annotation, final Function< Object, Class <?>> retriever) {
        //            final DeclaredType declaredType = (DeclaredType)getTypeElement(annotation, retriever).getSuperclass();
        //            if (declaredType.getTypeArguments().isEmpty())
        //            {
        //                return null;
        //            }
        //            return (TypeElement)((DeclaredType)declaredType.getTypeArguments().get(0)).asElement();
        //        }

        //        public List<Element> getElements(final Annotation annotation, final Function< Object, Class <?>> retriever) {
        //            return (List<Element>)getTypeElement(annotation, retriever).getEnclosedElements();
        //        }

        //        public TypeElement getTypeElement(final Annotation annotation,
        //                                           final Function< Object, Class <?>> retriever) {
        //            return from(annotation, retriever);
        //        }

        public bool IsValidPackage(string packageName) => _elements.Assembly.GetName().Name != null;

    }
}
