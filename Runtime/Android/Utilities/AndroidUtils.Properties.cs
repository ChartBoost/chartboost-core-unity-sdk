using UnityEngine;

namespace Chartboost.Core.Android.Utilities
{
    internal static partial class AndroidUtils
    {
        public static string ToString(this AndroidJavaObject nativeEnum) 
            => nativeEnum.Call<string>(AndroidConstants.FunctionToString);

        public static string GetEnumProperty(string environment, string field) 
        {
            using var property = GetProperty(environment, field);
            return ToString(property);
        }

        #nullable enable
        public static bool? GetBoolProperty(string environment, string field)
            => GetNullableProperty<bool>(environment, field, AndroidConstants.FunctionBooleanValue);

        public static double? GetDoubleProperty(string environment, string field)
            => GetNullableProperty<double>(environment, field, AndroidConstants.FunctionDoubleValue);

        public static float? GetFloatProperty(string environment, string field)
            => GetNullableProperty<float>(environment, field, AndroidConstants.FunctionFloatValue);

        public static int? GetIntegerProperty(string environment, string field) 
            => GetNullableProperty<int>(environment, field, AndroidConstants.FunctionIntValue);

        public static T GetNullableProperty<T>(string environment, string field, string function)
        {
            using var property = GetProperty(environment, field);
            return property == null ? default! : property.Call<T>(function);
        }

        public static T GetProperty<T>(string environment, string field)
        {
            using var sdk = GetNativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            return property.Call<T>(field);
        }

        private static AndroidJavaObject GetProperty(string environment, string field)
        {
            using var sdk = GetNativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            return property.Call<AndroidJavaObject>(field);
        }

        public static void SetProperty<T>(string environment, string field, T value)
        {
            using var sdk = GetNativeSDK();
            using var property = sdk.CallStatic<AndroidJavaObject>(environment);
            property.Call(field, value);
        }
        #nullable disable
    }
}
