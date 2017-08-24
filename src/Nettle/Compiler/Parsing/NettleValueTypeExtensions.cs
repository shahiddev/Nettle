﻿namespace Nettle.Compiler.Parsing
{
    using System;

    /// <summary>
    /// Represents various extension methods for the Nettle value type enum
    /// </summary>
    internal static class NettleValueTypeExtensions
    {
        /// <summary>
        /// Parses the value signature into the value type specified
        /// </summary>
        /// <param name="type">The value type</param>
        /// <param name="signature">The value signature</param>
        /// <returns>The parsed value</returns>
        public static object ParseValue
            (
                this NettleValueType type,
                string signature
            )
        {
            if (String.IsNullOrEmpty(signature))
            {
                return null;
            }

            var convertedValue = default(object);

            switch (type)
            {
                case NettleValueType.String:

                    if (signature.StartsWith("\"") && signature.EndsWith("\""))
                    {
                        convertedValue = signature.Crop
                        (
                            1,
                            signature.Length - 2
                        );
                    }
                    else
                    {
                        convertedValue = signature;
                    }

                    break;

                case NettleValueType.Number:

                    convertedValue = Double.Parse(signature);
                    break;

                case NettleValueType.Boolean:

                    convertedValue = Boolean.Parse(signature);
                    break;

                case NettleValueType.ModelBinding:

                    if (signature.StartsWith(@"{{") && signature.EndsWith(@"}}"))
                    {
                        convertedValue = signature.Crop
                        (
                            2,
                            signature.Length - 3
                        );
                    }
                    else
                    {
                        convertedValue = signature;
                    }

                    break;

                case NettleValueType.Variable:

                    // NOTE: this isn't resolvable until runtime
                    break;

                case NettleValueType.Function:

                    var functionParser = new FunctionParser();
                    var templateContent = String.Copy(signature);
                    var positionOffset = default(int);

                    convertedValue = functionParser.Parse
                    (
                        ref templateContent,
                        ref positionOffset,
                        signature
                    );

                    break;
            }

            return convertedValue;
        }
    }
}
