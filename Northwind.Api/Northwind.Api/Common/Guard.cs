//-------------------------------------------------------------------------------
// <copyright file="Guard.cs" company="frokonet.ch">
//   Copyright (c) 2016
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
// </copyright>
//-------------------------------------------------------------------------------

namespace Northwind.Api.Common
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;

    [DebuggerStepThrough]
    public static class Guard
    {
        public static void NotNull<T>(Expression<Func<T>> argumentExpression) where T : class
        {
            if (GetValue(argumentExpression) == null)
            {
                throw new ArgumentNullException(GetParameterName(argumentExpression), "Parameter must not be null.");
            }
        }

        public static void NotNullOrEmpty(Expression<Func<string>> argumentExpression)
        {
            var value = GetValue(argumentExpression);

            if (value == null)
            {
                throw new ArgumentNullException(GetParameterName(argumentExpression), "Parameter must not be null.");
            }

            if (value.Length == 0)
            {
                throw new ArgumentException("Parameter must not be empty", GetParameterName(argumentExpression));
            }
        }

        public static void IsTrue(bool expectation, string exceptionMessage)
        {
            if (!expectation)
            {
                throw new ArgumentException(exceptionMessage);
            }
        }
        
        private static T GetValue<T>(Expression<Func<T>> reference) where T : class
        {
            return reference.Compile().Invoke();
        }

        private static string GetParameterName(Expression expression)
        {
            var lambdaExpression = expression as LambdaExpression;
            var memberExpression = lambdaExpression?.Body as MemberExpression;

            return memberExpression?.Member.Name ?? string.Empty;
        }
    }
}