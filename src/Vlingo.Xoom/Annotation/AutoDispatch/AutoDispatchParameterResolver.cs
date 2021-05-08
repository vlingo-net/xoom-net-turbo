//// Copyright © 2012-2020 VLINGO LABS. All rights reserved.
////
//// This Source Code Form is subject to the terms of the
//// Mozilla Public License, v. 2.0. If a copy of the MPL
//// was not distributed with this file, You can obtain
//// one at https://mozilla.org/MPL/2.0/.

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Vlingo.Xoom.Codegen.Parameter;

//namespace Vlingo.Xoom.Annotation.AutoDispatch
//{
//    public class AutoDispatchParameterResolver
//    {
//        private static readonly string _signaturePattern = "{0}({1})";

//        private readonly TypeRetriever _typeRetriever;
//        private readonly ProcessingEnvironment _environment;
//        private readonly HashSet<Type> _autoDispatchResourceClasses = new HashSet<Type>();

//        private AutoDispatchParameterResolver(ProcessingEnvironment environment, HashSet<Type> autoDispatchResourceClasses)
//        {
//            _environment = environment;
//            _typeRetriever = TypeRetriever.With(environment);
//            _autoDispatchResourceClasses.UnionWith(autoDispatchResourceClasses);
//        }

//        public static AutoDispatchParameterResolver From(HashSet<Type> autoDispatchResourceClasses, ProcessingEnvironment environment) => new AutoDispatchParameterResolver(environment, autoDispatchResourceClasses);

//        public CodeGenerationParameters Resolve()
//        {
//            CodeGenerationParameters parameters = CodeGenerationParameters.From(Label.UseAutoDispatch, _autoDispatchResourceClasses != null && _autoDispatchResourceClasses.Count != 0);

//            _autoDispatchResourceClasses.stream()
//                    .map(this::resolveAutoDispatchAnnotation).forEach(parameters::add);
//            //_autoDispatchResourceClasses.Select(x=> resolveAutoDispatchAnnotation(x))
//            return parameters;
//        }

//        private CodeGenerationParameter ResolveAutoDispatchAnnotation(object autoDispatchClass)
//        {
//            var autoDispatchAnnotation = autoDispatchClass.GetAnnotation<AutoDispatch>();

//            Type handlersConfigClass = TypeRetriever.From(autoDispatchAnnotation, AutoDispatch::handlers);

//        CodeGenerationParameter autoDispatchParameter =
//            CodeGenerationParameter.of(AUTO_DISPATCH_NAME, autoDispatchClass.getQualifiedName())
//                    .relate(HANDLERS_CONFIG_NAME, handlersConfigClass.getQualifiedName())
//                    .relate(URI_ROOT, resolveRootURI(autoDispatchAnnotation));

//        resolveModelAnnotation(autoDispatchClass, autoDispatchParameter);
//        resolveQueriesAnnotation(autoDispatchClass, autoDispatchParameter);
//        resolveRoutesAnnotation(autoDispatchClass, handlersConfigClass, autoDispatchParameter);
//        return autoDispatchParameter;
//    }

//    //    private void resolveModelAnnotation(final TypeElement autoDispatchClass,
//    //                                        final CodeGenerationParameter autoDispatchParameter)
//    //    {
//    //        final Model modelAnnotation =
//    //                autoDispatchClass.getAnnotation(Model.class);

//    //        if(modelAnnotation != null) {
//    //            final TypeElement protocol =
//    //                    typeRetriever.from(modelAnnotation, Model::protocol);

//    //            final TypeElement actor =
//    //                    typeRetriever.from(modelAnnotation, Model::actor);

//    //            final TypeElement data =
//    //                    typeRetriever.from(modelAnnotation, Model::data);

//    //            autoDispatchParameter.relate(MODEL_PROTOCOL, protocol.getQualifiedName())
//    //                    .relate(MODEL_ACTOR, actor.getQualifiedName())
//    //                    .relate(MODEL_DATA, data.getQualifiedName());
//    //}
//    //    }

//    //    private void resolveQueriesAnnotation(final TypeElement autoDispatchClass,
//    //                                          final CodeGenerationParameter autoDispatchParameter)
//    //{
//    //    final Queries queriesAnnotation =
//    //            autoDispatchClass.getAnnotation(Queries.class);

//    //if (queriesAnnotation != null)
//    //{
//    //    final TypeElement protocol =
//    //            typeRetriever.from(queriesAnnotation, Queries::protocol);

//    //    final TypeElement actor =
//    //            typeRetriever.from(queriesAnnotation, Queries::actor);

//    //    autoDispatchParameter.relate(QUERIES_PROTOCOL, protocol.getQualifiedName())
//    //            .relate(QUERIES_ACTOR, actor.getQualifiedName());
//    //}
//    //    }

//    //    private void resolveRoutesAnnotation(final TypeElement autoDispatchClass,
//    //                                         final TypeElement handlersConfigClass,
//    //                                         final CodeGenerationParameter autoDispatchParameter)
//    //{
//    //    final String uriRoot = autoDispatchParameter.retrieveRelatedValue(URI_ROOT);
//    //    final Predicate<Element> filter = element->element instanceof ExecutableElement;
//    //    final Function<Element, ExecutableElement> mapper = element-> (ExecutableElement) element;
//    //    final HandlerResolver handlerResolver = HandlerResolver.with(handlersConfigClass, environment);

//    //    autoDispatchClass.getEnclosedElements().stream().filter(filter).map(mapper).forEach(method-> {
//    //        final Route routeAnnotation =
//    //                method.getAnnotation(Route.class);

//    //if (routeAnnotation != null)
//    //{
//    //    final Boolean internalRouteHandler = method.getModifiers().contains(DEFAULT);

//    //    final CodeGenerationParameter routeParameter =
//    //        CodeGenerationParameter.of(ROUTE_SIGNATURE, buildMethodSignature(method));

//    //    routeParameter.relate(ROUTE_METHOD, routeAnnotation.method())
//    //            .relate(INTERNAL_ROUTE_HANDLER, internalRouteHandler)
//    //            .relate(ROUTE_PATH, PathFormatter.formatAbsoluteRoutePath(uriRoot, routeAnnotation.path()));

//    //    if (!internalRouteHandler)
//    //    {
//    //        final HandlerInvocation handlerInvocation = handlerResolver.find(routeAnnotation.handler());

//    //        routeParameter.relate(ROUTE_HANDLER_INVOCATION, handlerInvocation.invocation)
//    //                .relate(USE_CUSTOM_ROUTE_HANDLER_PARAM, handlerInvocation.hasCustomParamNames());
//    //    }

//    //    resolveVariablesAnnotation(method, routeParameter);
//    //    resolveResponseAnnotation(method, routeParameter, handlerResolver);
//    //    autoDispatchParameter.relate(routeParameter);
//    //}
//    //        });
//    //    }

//    //    private String buildMethodSignature(final ExecutableElement method)
//    //{
//    //    final String signatureParameters =
//    //            method.getParameters().stream().map(param->
//    //                    param.asType() + " " + param.getSimpleName())
//    //                    .collect(Collectors.joining(", "));

//    //    return String.format(SIGNATURE_PATTERN, method.getSimpleName(), signatureParameters);
//    //}

//    //private void resolveVariablesAnnotation(final ExecutableElement method,
//    //                                        final CodeGenerationParameter routeParameter)
//    //{
//    //    method.getParameters().forEach(methodVariable-> {
//    //        resolveIdAnnotation(methodVariable, routeParameter);
//    //        resolveBodyAnnotation(methodVariable, routeParameter);
//    //    });
//    //    }

//    //    private void resolveIdAnnotation(final VariableElement methodVariable,
//    //                                     final CodeGenerationParameter routeParameter)
//    //{
//    //    final Id idAnnotation = methodVariable.getAnnotation(Id.class);

//    //if (idAnnotation != null)
//    //{
//    //    routeParameter.relate(ID, methodVariable.getSimpleName())
//    //            .relate(ID_TYPE, retriedIdTypeQualifiedName(methodVariable.asType()));
//    //}
//    //    }

//    //    private String retriedIdTypeQualifiedName(final TypeMirror idType)
//    //{
//    //    if (idType.getKind().isPrimitive())
//    //    {
//    //        return "";
//    //    }

//    //    final TypeElement idTypeElement =
//    //            (TypeElement)environment.getTypeUtils().asElement(idType);

//    //    return idTypeElement.getQualifiedName().toString();
//    //}

//    //private void resolveBodyAnnotation(final VariableElement methodVariable,
//    //                                   final CodeGenerationParameter routeParameter)
//    //{
//    //    final Body bodyAnnotation = methodVariable.getAnnotation(Body.class);

//    //if (bodyAnnotation != null)
//    //{
//    //    final TypeElement bodyType =
//    //            (TypeElement)environment.getTypeUtils()
//    //                    .asElement(methodVariable.asType());

//    //    routeParameter.relate(BODY, methodVariable.getSimpleName())
//    //            .relate(BODY_TYPE, bodyType.getQualifiedName());
//    //}
//    //    }

//    //    private void resolveResponseAnnotation(final ExecutableElement method,
//    //                                           final CodeGenerationParameter routeParameter,
//    //                                           final HandlerResolver handlerResolver)
//    //{
//    //    final ResponseAdapter responseAdapterAnnotation = method.getAnnotation(ResponseAdapter.class);

//    //if (responseAdapterAnnotation != null)
//    //{
//    //    final HandlerInvocation handlerInvocation =
//    //            handlerResolver.find(responseAdapterAnnotation.handler());

//    //    routeParameter.relate(USE_ADAPTER, true)
//    //            .relate(ADAPTER_HANDLER_INVOCATION, handlerInvocation.invocation)
//    //            .relate(USE_CUSTOM_ADAPTER_HANDLER_PARAM, handlerInvocation.hasCustomParamNames());
//    //}
//    //else
//    //{
//    //    routeParameter.relate(USE_ADAPTER, false);
//    //}
//    //    }

//    //    private String resolveRootURI(final AutoDispatch autoDispatch)
//    //{
//    //    final String rootURI = autoDispatch.path();
//    //    return rootURI.endsWith("/") ? rootURI : rootURI + "/";
//    //}

//    public T GetAnnotation<T>(this object instance) where T : Attribute => (T)typeof(T).GetCustomAttributes(typeof(T), false).First();
//}
//}
