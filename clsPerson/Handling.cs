using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.BaseOpration
{
    class Handling
    {
        public static class ExceptionHandler
        {
            // Handle functions with no parameters and no return value (Action)
            public static void Handle(Action action)
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error (Action): {ex.Message}");
                    throw ex; // Or handle as needed
                }
            }

            // Handle functions with one parameter and no return value (Action<T>)
            public static void Handle<T>(Action<T> action, T arg)
            {
                try
                {
                    action(arg);
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error (Action<T>): {ex.Message}");
                    throw ex; // Or handle as needed
                }
            }

            // Handle functions with two parameters and no return value (Action<T1, T2>)
            public static void Handle<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2)
            {
                try
                {
                    action(arg1, arg2);
                }
                catch (Exception ex)
                {
                   // Console.WriteLine($"Error (Action<T1,T2>): {ex.Message}");
                    throw ex; // Or handle as needed
                }
            }

            // Handle functions with no parameters and a return value (Func<TResult>)
            public static TResult Handle<TResult>(Func<TResult> func)
            {
                try
                {
                    return func();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine($"Error (Func<TResult>): {ex.Message}");
                    throw ex; // Or handle as needed
                }
            }

            // Handle functions with one parameter and a return value (Func<T, TResult>)
            public static TResult Handle<T, TResult>(Func<T, TResult> func, T arg)
            {
                try
                {
                    return func(arg);
                }
                catch (Exception ex)
                {
                   // Console.WriteLine($"Error (Func<T, TResult>): {ex.Message}");
                    throw ex; // Or handle as needed
                }
            }

            // Handle functions with two parameter and a return value (Func<T1, T2, TResult>)
            public static TResult Handle<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2)
            {
                try
                {
                    return func(arg1, arg2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error (Func<T1, T2, TResult>): {ex.Message}");
                    throw; // Or handle as needed
                }
            }
        }
    }
}
