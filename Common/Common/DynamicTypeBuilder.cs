using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Common
{
    public interface IDynamicTypeBuilderFactory
    {
        IDynamicTypeBuilder CreateTypeBuilder(string typeName);
        IDynamicTypeBuilder CreateTypeBuilder(string typeName, string assemblyName);
    }

    public interface IDynamicTypeBuilder
    {
        Type CreateType();
        IDynamicTypeBuilder AddProperty<T>(string propertyName);
        IDynamicTypeBuilder AddProperty(Type propertyType, string propertyName);
    }

    public class DynamicTypeBuilderFactory : IDynamicTypeBuilderFactory
    {
        public IDynamicTypeBuilder CreateTypeBuilder(string typeName)
        {
            return CreateTypeBuilder(typeName, "_DynamicTypeAssembly");
        }

        public IDynamicTypeBuilder CreateTypeBuilder(string typeName, string assemblyName)
        {
            return new Inner(typeName, assemblyName);
        }

        private class Inner : IDynamicTypeBuilder
        {
            public Inner(string typeName, string assemblyName)
            {
                _typeBuilder = AppDomain.CurrentDomain
                    .DefineDynamicAssembly(new AssemblyName(assemblyName), AssemblyBuilderAccess.Run)
                    .DefineDynamicModule(assemblyName)
                    .DefineType(typeName, TypeAttributes.Public);

                _typeBuilder.DefineDefaultConstructor(MethodAttributes.Public);
            }

            private readonly TypeBuilder _typeBuilder;

            public Type CreateType()
            {
                return _typeBuilder.CreateType();
            }

            public IDynamicTypeBuilder AddProperty<T>(string propertyName)
            {
                return AddProperty(typeof(T), propertyName);
            }

            public IDynamicTypeBuilder AddProperty(Type propertyType, string propertyName)
            {
                var fieldBuilder = _typeBuilder.DefineField(string.Concat("m_", propertyName), propertyType, FieldAttributes.Private);
                var propertyMethodAttributes = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

                var getterMethod = _typeBuilder.DefineMethod(string.Concat("get_", propertyName), propertyMethodAttributes, propertyType, Type.EmptyTypes);
                var getterILCode = getterMethod.GetILGenerator();
                getterILCode.Emit(OpCodes.Ldarg_0);
                getterILCode.Emit(OpCodes.Ldfld, fieldBuilder);
                getterILCode.Emit(OpCodes.Ret);

                var setterMethod = _typeBuilder.DefineMethod(string.Concat("set_", propertyName), propertyMethodAttributes, null, new[] {propertyType});
                var setterILCode = setterMethod.GetILGenerator();
                setterILCode.Emit(OpCodes.Ldarg_0);
                setterILCode.Emit(OpCodes.Ldarg_1);
                setterILCode.Emit(OpCodes.Stfld, fieldBuilder);
                setterILCode.Emit(OpCodes.Ret);

                var propertyBuilder = _typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);
                propertyBuilder.SetGetMethod(getterMethod);
                propertyBuilder.SetSetMethod(setterMethod);

                return this;
            }
        }
    }
}
