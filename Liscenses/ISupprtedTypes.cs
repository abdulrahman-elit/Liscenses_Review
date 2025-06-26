using System;

interface ISupportedTypes<T>
{
    // This Also Should Implemented in the Inherited Class
    // public static readonly Dictionary<string, (SqlDbType type, int? size)> person = new Dictionary<string, (SqlDbType type, int? size)>;
    bool Validation(T Check);
}
//public  class abstract SupprtedTypes<T>
//{
// public abstract readonly Dictionary<string, (SqlDbType type, int? size)> Key{set;get;}
// public abstract enum enStatus { Update, New };
// bool   abstract Validation(T Check);
//}