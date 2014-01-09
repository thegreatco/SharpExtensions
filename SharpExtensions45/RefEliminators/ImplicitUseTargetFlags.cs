using System;

namespace JetBrains.Annotations
{
    /// <summary>
    /// Specify what is considered used implicitly when marked with <see cref="T:JetBrains.Annotations.MeansImplicitUseAttribute"/> or <see cref="T:JetBrains.Annotations.UsedImplicitlyAttribute"/>
    /// </summary>
    [Flags]
    public enum ImplicitUseTargetFlags
    {
        Default = 1,
        Itself = Default,
        Members = 2,
        WithMembers = Members | Itself,
    }
}
