using System;

// ReSharper disable once CheckNamespace
namespace JetBrains.Annotations
{
    /// <summary>
    /// Indicates that the marked method builds string by format pattern and (optional) arguments.
    /// Parameter, which contains format string, should be given in constructor.
    /// The format string should be in <see cref="M:System.String.Format(System.IFormatProvider,System.String,System.Object[])"/> -like form
    /// 
    /// </summary>
    /// 
    /// <example>
    /// 
    /// <code>
    /// [StringFormatMethod("message")]
    /// public void ShowError(string message, params object[] args)
    /// {
    ///     //Do something
    /// }
    /// public void Foo()
    /// {
    ///     ShowError("Failed: {0}"); // Warning: Non-existing argument in format string
    /// }
    /// 
    /// </code>
    /// 
    /// </example>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        /// <summary>
        /// Gets format parameter name
        /// </summary>
        [UsedImplicitly]
        public string FormatParameterName { get; private set; }

        /// <summary>
        /// Initializes new instance of StringFormatMethodAttribute
        /// </summary>
        /// <param name="formatParameterName">Specifies which parameter of an annotated method should be treated as format-string</param>
        public StringFormatMethodAttribute(string formatParameterName)
        {
            FormatParameterName = formatParameterName;
        }
    }
}